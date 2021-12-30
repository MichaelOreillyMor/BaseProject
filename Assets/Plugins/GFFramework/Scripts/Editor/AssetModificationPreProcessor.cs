using UnityEditor;

namespace GFF.Editor
{
    public class AssetModificationPreProcessor : UnityEditor.AssetModificationProcessor
    {
        public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions rao)
        {
            GameStatesEnumGenerator.EditorInstance.OnPreFileDeleted(assetPath);
            return AssetDeleteResult.DidNotDelete;
        }
    }
}