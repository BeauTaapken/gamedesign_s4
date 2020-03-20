using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EzySlice {

	/**
	 * The final generated data structure from a slice operation. This provides easy access
	 * to utility functions and the final Mesh data for each section of the HULL.
	 */
	public sealed class SlicedHull {
		private Mesh upper_hull;
		private Mesh lower_hull;

		public SlicedHull(Mesh upperHull, Mesh lowerHull) {
			this.upper_hull = upperHull;
			this.lower_hull = lowerHull;
		}

        public GameObject createHull(GameObject original, Material crossSectionMat, bool isUpperHull) {
            GameObject newObject;
            if (isUpperHull)
            {
                newObject = CreateUpperHull();
            }
            else
            {
                newObject = CreateLowerHull();
            }

			if (newObject != null) {
				newObject.transform.position = original.transform.position;
				newObject.transform.rotation = original.transform.rotation;
				newObject.transform.localScale = original.transform.localScale;

                //Material[] shared = original.GetComponent<MeshRenderer>().sharedMaterials;
                Material[] shared = original.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
                Mesh mesh = original.GetComponent<SkinnedMeshRenderer>().sharedMesh;

                // nothing changed in the hierarchy, the cross section must have been batched
                // with the submeshes, return as is, no need for any changes
                if (isUpperHull)
                {
                    if (mesh.subMeshCount == upper_hull.subMeshCount)
                    {
                        // the the material information
                        newObject.GetComponent<Renderer>().sharedMaterials = shared;

                        return newObject;
                    }
                }
                else
                {
                    if (mesh.subMeshCount == lower_hull.subMeshCount)
                    {
                        // the the material information
                        newObject.GetComponent<Renderer>().sharedMaterials = shared;

                        return newObject;
                    }
                }

                // otherwise the cross section was added to the back of the submesh array because
                // it uses a different material. We need to take this into account
                Material[] newShared = new Material[shared.Length + 1];

                // copy our material arrays across using native copy (should be faster than loop)
                System.Array.Copy(shared, newShared, shared.Length);
                newShared[shared.Length] = crossSectionMat;

                // the the material information
                newObject.GetComponent<Renderer>().sharedMaterials = newShared;
			}

			return newObject;
		}

        /**
         * Generate a new GameObject from the upper hull of the mesh
         * This function will return null if upper hull does not exist
         */
        public GameObject CreateUpperHull() {
            return CreateEmptyObject("Upper_Hull", upper_hull);
        }

		/**
		 * Generate a new GameObject from the Lower hull of the mesh
		 * This function will return null if lower hull does not exist
		 */
		public GameObject CreateLowerHull() {
			return CreateEmptyObject("Lower_Hull", lower_hull);
		}

		public Mesh upperHull {
			get { return this.upper_hull; }
		}

		public Mesh lowerHull {
			get { return this.lower_hull; }
		}

		/**
		 * Helper function which will create a new GameObject to be able to add
		 * a new mesh for rendering and return.
		 */
		private static GameObject CreateEmptyObject(string name, Mesh hull) {
			if (hull == null) {
				return null;
			}

			GameObject newObject = new GameObject(name);

            SkinnedMeshRenderer filter = newObject.AddComponent<SkinnedMeshRenderer>();
            

            filter.sharedMesh = hull;

			return newObject;
		}
	}
}