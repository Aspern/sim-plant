using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor {
    public class SimPlantAssetsPostprocessor : AssetPostprocessor {

        private void OnPreprocessModel() {
            var modelImporter = assetImporter as ModelImporter;
            if (!modelImporter) return;

            modelImporter.globalScale = 1.0f;
            modelImporter.useFileScale = false;
            modelImporter.useFileUnits = false;

            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
            modelImporter.importBlendShapes = false;
            modelImporter.importCameras = false;
            modelImporter.importLights = false;
            modelImporter.importAnimation = false;
            modelImporter.animationType = ModelImporterAnimationType.None;

            modelImporter.addCollider = false;
        }
        
        private void OnPostprocessModel(GameObject gameObject) {
            MeshFilter[] meshes = gameObject.GetComponentsInChildren<MeshFilter>(true);

            foreach (var meshFilter in meshes) {
                RotateMesh(meshFilter, Quaternion.Euler(0f, 180f, 0f));
                AddMaterials(meshFilter);
            }
            
            gameObject.transform.localScale = Vector3.one;
        }
        
        private static void RotateMesh(MeshFilter meshFilter, Quaternion rot) {
            Mesh mesh = null;

            if (meshFilter) {
                mesh = meshFilter.sharedMesh;
            }

            if (mesh) {
                Vector3[] vertices = mesh.vertices;
                Vector3[] normals = mesh.normals;

                for (int i = 0, c = vertices.Length; i < c; i++) {
                    vertices[i] = rot * vertices[i];
                    if (normals != null && i < normals.Length) {
                        normals[i] = (rot * normals[i]).normalized;
                    }
                }

                mesh.vertices = vertices;
                mesh.normals = normals;
                mesh.RecalculateBounds();
            }
        }

        private void AddMaterials(MeshFilter mesh) {
            var meshRenderer = mesh.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = Resources.Load("Materials/SimPlantDefaultMaterial") as Material;
        }
    }
}