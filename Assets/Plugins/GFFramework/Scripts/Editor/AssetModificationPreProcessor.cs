using UnityEditor;

namespace GFF.Editor
{
    public class AssetModificationPreProcessor : UnityEditor.AssetModificationProcessor
    {
        public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions rao)
        {
            GameStateKeysGenerator.EditorInstance.OnPreFileDeleted(assetPath);
            SceneKeysGenerator.EditorInstance.OnPreFileDeleted(assetPath);
            return AssetDeleteResult.DidNotDelete;
        }
    }
}