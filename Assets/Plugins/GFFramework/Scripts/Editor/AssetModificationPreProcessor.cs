using UnityEditor;

namespace GFF.Editor
{
    public class AssetModificationPreProcessor : UnityEditor.AssetModificationProcessor
    {
        public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions rao)
        {
            GameStatesEnumGenerator gameStatesEnumProccessor = new GameStatesEnumGenerator();
            gameStatesEnumProccessor.OnPreFileDeleted(assetPath);
            return AssetDeleteResult.DidNotDelete;
        }
    }
}