using UnityEditor;

namespace GFF.Editor
{
    public class AssetModificationPreProcessor : UnityEditor.AssetModificationProcessor
    {
        public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions rao)
        {
            GameStatesEnumProccessor gameStatesEnumProccessor = new GameStatesEnumProccessor();
            gameStatesEnumProccessor.OnPreFileDeleted(assetPath);
            return AssetDeleteResult.DidNotDelete;
        }
    }
}