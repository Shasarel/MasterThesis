using SharpDX;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using SharpDX.D3DCompiler;
using D3D11 = SharpDX.Direct3D11;
using System.Diagnostics;
using System.Windows.Forms;
using PrimordialEngine.Interfaces;

namespace PrimordialEngine.DirectXRenderer
{
    class DirectXRenderer : IPrimordialRenderer
    {
        private RenderForm _form;
        private SwapChain _swapChain;
        private D3D11.Device _device;
        private D3D11.DeviceContext _context;
        private D3D11.PixelShader _pixelShader;
        private D3D11.VertexShader _vertexShader;
        private Factory _factory;
        private ShaderBytecode _vertexShaderByteCode, _pixelShaderByteCode;
        private ShaderSignature _shaderSignature;
        private D3D11.InputLayout _inputLayout;
        private D3D11.Buffer _verticesBuffer, _contantBuffer;
        private Stopwatch _clock;
        private D3D11.Texture2D _depthBuffer, _backBuffer;
        private D3D11.RenderTargetView _renderView;
        private D3D11.DepthStencilView _depthView;
        private bool _userResized = true;
        private SwapChainDescription _swapChainDescription;
        private Matrix _view, _proj = Matrix.Identity;
        private PrimordialObject _primordialObject;

        public DirectXRenderer(){}

        public void Initialize(int width, int height, PrimordialObject primordialObject)
        {
            _form = new RenderForm("PrimordialEngine");
            _form.ClientSize = new System.Drawing.Size(width, height);

            _primordialObject = primordialObject;

            _swapChainDescription = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(_form.ClientSize.Width, _form.ClientSize.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = _form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            D3D11.Device.CreateWithSwapChain(DriverType.Hardware, D3D11.DeviceCreationFlags.None, _swapChainDescription, out _device, out _swapChain);
            _context = _device.ImmediateContext;

            _factory = _swapChain.GetParent<Factory>();
            _factory.MakeWindowAssociation(_form.Handle, WindowAssociationFlags.IgnoreAll);

            _vertexShaderByteCode = ShaderBytecode.CompileFromFile("DirectXRenderer\\MiniCube.hlsl", "VS", "vs_4_0");
            _vertexShader = new D3D11.VertexShader(_device, _vertexShaderByteCode);

            _pixelShaderByteCode = ShaderBytecode.CompileFromFile("DirectXRenderer\\MiniCube.hlsl", "PS", "ps_4_0");
            _pixelShader = new D3D11.PixelShader(_device, _pixelShaderByteCode);

            _shaderSignature = ShaderSignature.GetInputSignature(_vertexShaderByteCode);

            _inputLayout = new D3D11.InputLayout(_device, _shaderSignature, new[]
                    {
                        new D3D11.InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new D3D11.InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });

            _verticesBuffer = D3D11.Buffer.Create(_device, D3D11.BindFlags.VertexBuffer, _primordialObject.VertexData);

            _contantBuffer = new D3D11.Buffer(_device, Utilities.SizeOf<Matrix>(), D3D11.ResourceUsage.Default, D3D11.BindFlags.ConstantBuffer, D3D11.CpuAccessFlags.None, D3D11.ResourceOptionFlags.None, 0);

            _context.InputAssembler.InputLayout = _inputLayout;
            _context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            _context.InputAssembler.SetVertexBuffers(0, new D3D11.VertexBufferBinding(_verticesBuffer, Utilities.SizeOf<VertexDataStruct>(), 0));
            _context.VertexShader.SetConstantBuffer(0, _contantBuffer);
            _context.VertexShader.Set(_vertexShader);
            _context.PixelShader.Set(_pixelShader);

            _view = Matrix.LookAtLH(new Vector3(0, 0, 0), _primordialObject.Position, Vector3.UnitY);

            _clock = new Stopwatch();
            _clock.Start();

            _backBuffer = null;
            _renderView = null;
            _depthBuffer = null;
            _depthView = null;

            _form.UserResized += (sender, args) => _userResized = true;

            _form.KeyUp += (sender, args) =>
            {
                if (args.KeyCode == Keys.F5)
                    _swapChain.SetFullscreenState(true, null);
                else if (args.KeyCode == Keys.F4)
                    _swapChain.SetFullscreenState(false, null);
                else if (args.KeyCode == Keys.Escape)
                    _form.Close();
            };
        }

        public void Start()
        {
            RenderLoop.Run(_form, Run);
        }

        public void Run()
        {
            if (_userResized)
            {
                Utilities.Dispose(ref _backBuffer);
                Utilities.Dispose(ref _renderView);
                Utilities.Dispose(ref _depthBuffer);
                Utilities.Dispose(ref _depthView);

                _swapChain.ResizeBuffers(_swapChainDescription.BufferCount, _form.ClientSize.Width, _form.ClientSize.Height, Format.Unknown, SwapChainFlags.None);

                _backBuffer = D3D11.Resource.FromSwapChain<D3D11.Texture2D>(_swapChain, 0);

                _renderView = new D3D11.RenderTargetView(_device, _backBuffer);

                _depthBuffer = new D3D11.Texture2D(_device, new D3D11.Texture2DDescription()
                {
                    Format = Format.D32_Float_S8X24_UInt,
                    ArraySize = 1,
                    MipLevels = 1,
                    Width = _form.ClientSize.Width,
                    Height = _form.ClientSize.Height,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = D3D11.ResourceUsage.Default,
                    BindFlags = D3D11.BindFlags.DepthStencil,
                    CpuAccessFlags = D3D11.CpuAccessFlags.None,
                    OptionFlags = D3D11.ResourceOptionFlags.None
                });

                _depthView = new D3D11.DepthStencilView(_device, _depthBuffer);

                _context.Rasterizer.SetViewport(new Viewport(0, 0, _form.ClientSize.Width, _form.ClientSize.Height, 0.0f, 1.0f));
                _context.OutputMerger.SetTargets(_depthView, _renderView);

                const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;

                _proj = Matrix.PerspectiveFovLH(rads, _form.ClientSize.Width / (float)_form.ClientSize.Height, 0.1f, 100.0f);

                _userResized = false;
            }

            var time = _clock.ElapsedMilliseconds / 1000.0f;

            //_primordialObject.Position = new Vector3((float)Math.Cos(time * 2), (float)Math.Sin(time*2), (float)Math.Sin(time * 2)* (float)Math.Cos(time * 2));

            _view = Matrix.LookAtLH(new Vector3(0, 0, 0), new Vector3(0, 0, -1), Vector3.UnitY);
             
            var viewProj = Matrix.Multiply(_view, _proj);

            _context.ClearDepthStencilView(_depthView, D3D11.DepthStencilClearFlags.Depth, 1.0f, 0);
            _context.ClearRenderTargetView(_renderView, SharpDX.Color.Black);

            var worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * .1f) * Matrix.RotationZ(time * .1f) * Matrix.Translation(_primordialObject.Position) * viewProj;
            worldViewProj.Transpose();
            var a = worldViewProj.ToArray();
            _context.UpdateSubresource(a, _contantBuffer);

            _context.Draw(_primordialObject.VertexData.Length, 0);

            _swapChain.Present(0, PresentFlags.None);
        }
        public void Dispose()
        {
            _shaderSignature.Dispose();
            _vertexShaderByteCode.Dispose();
            _vertexShader.Dispose();
            _pixelShaderByteCode.Dispose();
            _pixelShader.Dispose();
            _verticesBuffer.Dispose();
            _inputLayout.Dispose();
            _contantBuffer.Dispose();
            _depthBuffer.Dispose();
            _depthView.Dispose();
            _context.ClearState();
            _context.Flush();
            _swapChain.Dispose();
            _renderView.Dispose();
            _backBuffer.Dispose();
            _device.Dispose();
            _context.Dispose();
            _factory.Dispose();
        }
    }
}
