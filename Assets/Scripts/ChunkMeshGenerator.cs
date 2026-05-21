
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
    public static class ChunkMeshGenerator
    {
        public static Mesh GenerateMesh(Chunk chunk)
        {
            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();
            var triangles = new List<int>();

            for (uint z = 0; z < Chunk.ChunkSize; z++)
            {
                for (uint y = 0; y < Chunk.ChunkSize; y++)
                {
                    for (uint x = 0; x < Chunk.ChunkSize; x++)
                    {
                        GenerateBlock(chunk, x, y, z, vertices, normals, uvs, triangles);
                    }
                }
            }

            Mesh mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            return mesh;
        }

        private static Vector4 GetFaceUVRect(BlockProperties props, Facing face)
        {
            var texCoord = props.GetFaceTexCoord(face);
            var u0 = texCoord.x / 16f;
            var u1 = (texCoord.x + 1) / 16f;
            var v0 = 1.0f - (texCoord.y + 1) / 16f;
            var v1 = 1.0f - texCoord.y / 16f;

            return new Vector4(u0, u1, v0, v1);
        }

        public static void GenerateBlock(Chunk chunk, uint x, uint y, uint z, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs, List<int> triangles)
        {
            ref Voxel voxel = ref chunk.GetVoxelAt(x, y, z);

            if (voxel.block == Blocks.Air)
                return;

            var props = BlockProperties.Get(voxel.block);
            var uvRect = GetFaceUVRect(props, Facing.West);

            var startIndex = vertices.Count;

            // Bottom Face (Y-)
            if (y == 0 || chunk.GetVoxelAt(x, y - 1, z).block == Blocks.Air)
            {
                var bottomUvRect = GetFaceUVRect(props, Facing.Bottom);
                vertices.Add(new Vector3(x, y, z));
                vertices.Add(new Vector3(x + 1, y, z));
                vertices.Add(new Vector3(x + 1, y, z + 1));
                vertices.Add(new Vector3(x, y, z + 1));

                uvs.Add(new Vector2(bottomUvRect.x, bottomUvRect.z));
                uvs.Add(new Vector2(bottomUvRect.y, bottomUvRect.z));
                uvs.Add(new Vector2(bottomUvRect.y, bottomUvRect.w));
                uvs.Add(new Vector2(bottomUvRect.x, bottomUvRect.w));

                normals.Add(Vector3.down);
                normals.Add(Vector3.down);
                normals.Add(Vector3.down);
                normals.Add(Vector3.down);

                triangles.Add(startIndex + 0);
                triangles.Add(startIndex + 1);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 3);
                triangles.Add(startIndex + 0);

                startIndex += 4;
            }

            // Top Face (Y+)
            if (y == 15 || chunk.GetVoxelAt(x, y+1, z).block == Blocks.Air)
            {
                var topUvRect = GetFaceUVRect(props, Facing.Top);

                vertices.Add(new Vector3(x, y + 1, z));
                vertices.Add(new Vector3(x + 1, y + 1, z));
                vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                vertices.Add(new Vector3(x, y + 1, z + 1));

                uvs.Add(new Vector2(topUvRect.x, topUvRect.z));
                uvs.Add(new Vector2(topUvRect.y, topUvRect.z));
                uvs.Add(new Vector2(topUvRect.y, topUvRect.w));
                uvs.Add(new Vector2(topUvRect.x, topUvRect.w));

                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);

                triangles.Add(startIndex + 0);
                triangles.Add(startIndex + 3);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 1);
                triangles.Add(startIndex + 0);

                startIndex += 4;
            }


            // South (Z-)
            if (z == 0 || chunk.GetVoxelAt(x, y, z - 1).block == Blocks.Air)
            {
                var southUvRect = GetFaceUVRect(props, Facing.South);

                vertices.Add(new Vector3(x, y, z));
                vertices.Add(new Vector3(x + 1, y, z));
                vertices.Add(new Vector3(x + 1, y + 1, z));
                vertices.Add(new Vector3(x, y + 1, z));

                uvs.Add(new Vector2(southUvRect.x, southUvRect.z));
                uvs.Add(new Vector2(southUvRect.y, southUvRect.z));
                uvs.Add(new Vector2(southUvRect.y, southUvRect.w));
                uvs.Add(new Vector2(southUvRect.x, southUvRect.w));

                normals.Add(Vector3.back);
                normals.Add(Vector3.back);
                normals.Add(Vector3.back);
                normals.Add(Vector3.back);

                triangles.Add(startIndex + 0);
                triangles.Add(startIndex + 3);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 1);
                triangles.Add(startIndex + 0);

                startIndex += 4;
            }

            // North (Z+)
            if (z == 15 || chunk.GetVoxelAt(x, y, z + 1).block == Blocks.Air)
            {
                var northUvRect = GetFaceUVRect(props, Facing.North);

                vertices.Add(new Vector3(x, y, z + 1));
                vertices.Add(new Vector3(x + 1, y, z + 1));
                vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                vertices.Add(new Vector3(x, y + 1, z + 1));

                uvs.Add(new Vector2(northUvRect.x, northUvRect.z));
                uvs.Add(new Vector2(northUvRect.y, northUvRect.z));
                uvs.Add(new Vector2(northUvRect.y, northUvRect.w));
                uvs.Add(new Vector2(northUvRect.x, northUvRect.w));

                normals.Add(Vector3.forward);
                normals.Add(Vector3.forward);
                normals.Add(Vector3.forward);
                normals.Add(Vector3.forward);

                triangles.Add(startIndex + 0);
                triangles.Add(startIndex + 1);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 3);
                triangles.Add(startIndex + 0);

                startIndex += 4;
            }

            // West (X-)
            if (x == 0 || chunk.GetVoxelAt(x - 1, y, z).block == Blocks.Air)
            {
                var westUvRect = GetFaceUVRect(props, Facing.West);

                vertices.Add(new Vector3(x, y, z));
                vertices.Add(new Vector3(x, y, z + 1));
                vertices.Add(new Vector3(x, y + 1, z + 1));
                vertices.Add(new Vector3(x, y + 1, z));

                uvs.Add(new Vector2(westUvRect.x, westUvRect.z));
                uvs.Add(new Vector2(westUvRect.y, westUvRect.z));
                uvs.Add(new Vector2(westUvRect.y, westUvRect.w));
                uvs.Add(new Vector2(westUvRect.x, westUvRect.w));

                normals.Add(Vector3.left);
                normals.Add(Vector3.left);
                normals.Add(Vector3.left);
                normals.Add(Vector3.left);

                triangles.Add(startIndex + 0);
                triangles.Add(startIndex + 1);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 3);
                triangles.Add(startIndex + 0);

                startIndex += 4;
            }

            // East (X+)
            if (x == 15 || chunk.GetVoxelAt(x + 1, y, z).block == Blocks.Air)
            {
                var eastUvRect = GetFaceUVRect(props, Facing.East);

                vertices.Add(new Vector3(x + 1, y, z));
                vertices.Add(new Vector3(x + 1, y, z + 1));
                vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                vertices.Add(new Vector3(x + 1, y + 1, z));

                uvs.Add(new Vector2(eastUvRect.x, eastUvRect.z));
                uvs.Add(new Vector2(eastUvRect.y, eastUvRect.z));
                uvs.Add(new Vector2(eastUvRect.y, eastUvRect.w));
                uvs.Add(new Vector2(eastUvRect.x, eastUvRect.w));

                normals.Add(Vector3.right);
                normals.Add(Vector3.right);
                normals.Add(Vector3.right);
                normals.Add(Vector3.right);

                triangles.Add(startIndex + 0);
                triangles.Add(startIndex + 3);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 2);
                triangles.Add(startIndex + 1);
                triangles.Add(startIndex + 0);
                startIndex += 4;
            }
        }
    }
}