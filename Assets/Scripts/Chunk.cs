using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
    public struct Voxel
    {
        public Blocks block;
        public ushort metadata;
    }

    public enum Blocks : ushort
    {
        Air = 0,
        Stone = 1,
        Dirt = 2,
        Wood = 3,
        Grass = 4
    }

    public enum Facing : byte
    {
        West,
        East,
        Bottom,
        Top,
        South,
        North
    }

    public class BlockProperties
    {
        private static Dictionary<Blocks, BlockProperties> s_Properties = new Dictionary<Blocks, BlockProperties>
        {
            { Blocks.Air, new BlockProperties() },
            { Blocks.Stone, new BlockProperties() { WestTexCoord = new Vector2Int(0, 0), TopTexCoord = new Vector2Int(0, 0) } },
            { Blocks.Dirt, new BlockProperties() { WestTexCoord = new Vector2Int(1, 0), TopTexCoord = new Vector2Int(1, 0) } },
            { Blocks.Wood, new BlockProperties() { WestTexCoord = new Vector2Int(5, 0), TopTexCoord = new Vector2Int(6, 0), DownTexCoord = new Vector2Int(6,0) }},
            { Blocks.Grass, new BlockProperties() { WestTexCoord = new Vector2Int(2, 0), TopTexCoord = new Vector2Int(3, 0),DownTexCoord = new Vector2Int(1,0) } },
        };

        private static BlockProperties s_FallbackOnMissing = new BlockProperties();

        public static BlockProperties Get(Blocks blocks)
        {
            if (s_Properties.TryGetValue(blocks, out var properties))
            {
                return properties;
            }

            return s_FallbackOnMissing;
        }

        public Vector2Int WestTexCoord { get; set; }
        public Vector2Int TopTexCoord { get; set; }

        public Vector2Int DownTexCoord { get; set; }

        public Vector2Int GetFaceTexCoord(Facing face)
        {
            switch (face)
            {
                case Facing.West: return WestTexCoord;
                case Facing.Top: return TopTexCoord;
                case Facing.Bottom: return DownTexCoord;
                default: return WestTexCoord;
            }
            
        }
    }

    public class Chunk
    {
        public const uint ChunkSize = 16;
        private const uint VoxelCount = ChunkSize * ChunkSize * ChunkSize;

        private Voxel[] m_Voxels = new Voxel[VoxelCount];

        private static uint IndexOf(uint x, uint y, uint z)
        {
            return x + y * ChunkSize + z * ChunkSize * ChunkSize;
        }

        public ref Voxel GetVoxelAt(uint x, uint y, uint z)
        {
            return ref m_Voxels[IndexOf(x, y, z)];
        }
    }
}

