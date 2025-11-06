using Microsoft.Extensions.Localization;

namespace learning_center_webapi.Contexts.Shared.Domain.Services;

public static class LocalizationService
{
    private static IStringLocalizerFactory? _localizerFactory;
    
    public static void Initialize(IStringLocalizerFactory localizerFactory)
    {
        _localizerFactory = localizerFactory;
    }
    
    public static IStringLocalizer GetLocalizer(string baseName, string location)
    {
        if (_localizerFactory == null)
            throw new InvalidOperationException("LocalizationService has not been initialized");
            
        return _localizerFactory.Create(baseName, location);
    }
}