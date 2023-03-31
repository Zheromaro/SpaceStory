using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Tilemaps;
#endif

[CustomGridBrush(false, true, true, "Default Brush")]
public class DefaultBrush : GridBrush
{
    private Tilemap targetTilemap;

    // These are the names of tile objects that i use in the project
    private string[] tileName =
        {
            "BG Rock Rule",
            "BG WoodDark Rule",
            "BG WoodLight Rule",
            "G Grase Rule",
            "G OldRock Rule",
            "G Rock Rule",
            "G Wood Rule",
            "+Speed Rule",
            "-Speed Rule",
            "DForce Rule",
            "LForce Rule",
            "RForce Rule",
            "UForce Rule"
            // etc.
        };

    private Dictionary<string, string> tiles = new Dictionary<string, string>();

    public DefaultBrush()
    {
        tiles.Add("BG", "BackGround TM");
        tiles.Add("G", "Ground TM");
        tiles.Add("+Speed", "+Speed TM");
        tiles.Add("-Speed", "-Speed TM");
        tiles.Add("DForce", "D Force TM");
        tiles.Add("LForce", "L Force TM");
        tiles.Add("RForce", "R Force TM");
        tiles.Add("UForce", "U Force TM");
    }

    public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pickStart)
    {
        base.Pick(gridLayout, brushTarget, position, pickStart);
        TileBase currentTile = cells[0].tile;
        if (currentTile != null)
        {
            string tilemapName = this.GetCorrespondingTilemapName(currentTile.name);
            List<Tilemap> tilemaps = GameObject.FindObjectsOfType<Tilemap>().ToList();
            this.targetTilemap = tilemaps.Find(tilemap => tilemap.name.Equals(tilemapName));
            Selection.activeObject = this.targetTilemap.gameObject;
        }
    }

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.Paint(gridLayout, brushTarget, position);
        }
    }

    public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.BoxFill(gridLayout, brushTarget, position);
        }
    }

    public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.FloodFill(gridLayout, brushTarget, position);
        }
    }

    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.Erase(gridLayout, brushTarget, position);
        }
    }


    public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.BoxErase(gridLayout, brushTarget, position);
        }
    }

    // Here i get the corresponding tilemap name.
    private string GetCorrespondingTilemapName(string tileName)
    {
        for (int i = 0; i < this.tileName.Length; i++)
        {
            if(this.tileName[i] == tileName)
            {
                return tiles.GetValueOrDefault(tileName.Split(" ")[0]);
            }
        }

        Debug.LogWarning("There is a problem");
        return "Ground TM";
    }
}