using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

// Load kernel configuration settings
var kernelSettings = KernelSettings.LoadSettings();
var kernelConfig = new KernelConfig();

// Add completion model
kernelConfig.AddCompletionBackend(kernelSettings);

// Initialize the logger
using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .SetMinimumLevel(kernelSettings.LogLevel ?? LogLevel.Warning)
        .AddConsole()
        .AddDebug();
});

// Create the kernel
IKernel kernel = new KernelBuilder().WithLogger(loggerFactory.CreateLogger<IKernel>()).WithConfiguration(kernelConfig).Build();

// Demo: Call a semantic function from a plugin
await Demos.SemanticFunctionDemos.CallSemanticFunction(kernel);

// Demo: Chain semantic functions together
//await Demos.SemanticFunctionDemos.BasicChaining(kernel);