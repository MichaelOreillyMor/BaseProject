using System.Linq;
using UnityEditor;

namespace GFF.Editor
{
    public class AssetModificationPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            GameStatesEnumGenerator gameStatesEnumProccessor = new GameStatesEnumGenerator();

            foreach (string assetPath in importedAssets)
            {
                if (!movedAssets.Contains(assetPath) && !movedFromAssetPaths.Contains(assetPath))
                {
                    gameStatesEnumProccessor.OnPostFileCreated(assetPath);
                }       
            }

            for (int i = 0; i < movedAssets.Length; i++)
            {
                gameStatesEnumProccessor.OnPostFileMoved(movedAssets[i], movedFromAssetPaths[i]);
            }
        }
    }
}