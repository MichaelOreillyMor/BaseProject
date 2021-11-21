///
/// Simple pooling for Unity.
///   Author: Martin "quill18" Glaude (quill18@quill18.com). Modified by Michael OReilly
///   Latest Version: https://gist.github.com/quill18/5a7cfffae68892621267
///   License: CC0 (http://creativecommons.org/publicdomain/zero/1.0/)
///   UPDATES:
/// 	2015-04-16: Changed Pool to use a Stack generic.
/// 
/// Usage:
/// 
///   There's no need to do any special setup of any kind.
/// 
///   Instead of calling Instantiate(), use this:
///       SimplePool.Spawn(somePrefab, somePosition, someRotation);
/// 
///   Instead of destroying an object, use this:
///       SimplePool.Despawn(myGameObject);
/// 
///   If desired, you can preload the pool with a number of instances:
///       SimplePool.Preload(somePrefab, 20);
/// 
/// Remember that Awake and Start will only ever be called on the first instantiation
/// and that member variables won't be reset automatically.  You should reset your
/// object yourself after calling Spawn().  (i.e. You'll have to do things like set
/// the object's HPs to max, reset animation states, etc...)
/// 
/// 
/// 
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GFF.PoolsMan.Pools
{   
    /// <summary>
    /// The Pool class represents the pool for a particular prefab.
    /// </summary>
    public class Pool
    {
        // We append an id to the name of anything we instantiate.
        // This is purely cosmetic.
        private int nextId = 1;

        // The structure containing our inactive objects.
        // Why a Stack and not a List? Because we'll never need to
        // pluck an object from the start or middle of the array.
        // We'll always just grab the last one, which eliminates
        // any need to shuffle the objects around in memory.
        private Stack<PoolMember> inactive;
        private List<PoolMember> active;

        // The prefab that we are pooling
        private PoolMember prefab;

        //Callback to allow PoolMembers to despawn themselves properly
        private Action<PoolMember, Pool> onDespawnToPoolCallback;

        // Constructor
        public Pool(PoolMember prefab, int initialQty, Action<PoolMember, Pool> onDespawnToPoolCallback)
        {
            this.prefab = prefab;
            this.onDespawnToPoolCallback = onDespawnToPoolCallback;

            inactive = new Stack<PoolMember>(initialQty);
            active = new List<PoolMember>(initialQty);
        }

        public void DestroyPoolMembers() 
        {
            nextId = 0;
            for (int i = 0; i < inactive.Count; i++)
            {
                PoolMember poolMember = inactive.Pop();
                GameObject.Destroy(poolMember.gameObject);
            }

            inactive.Clear();

            for (int i = 0; i < active.Count; i++)
            {
                PoolMember poolMember = active[i];
                GameObject.Destroy(poolMember.gameObject);
            }

            active.Clear();
        }

        // Spawn an object from our pool
        public PoolMember Spawn(Vector3 pos, Quaternion rot)
        {
            PoolMember poolMember;
            if (inactive.Count == 0)
            {
                // We don't have an object in our pool, so we
                // instantiate a whole new object.
                poolMember = GameObject.Instantiate(prefab, pos, rot);
                poolMember.name = prefab.name + " (" + (nextId++) + ")";
                poolMember.InitPoolMember(this, onDespawnToPoolCallback);
            }
            else
            {
                // Grab the last object in the inactive array
                poolMember = inactive.Pop();
    

                if (poolMember == null)
                {
                    // The inactive object we expected to find no longer exists.
                    // The most likely causes are:
                    //   - Someone calling Destroy() on our object
                    //   - A scene change (which will destroy all our objects).
                    //     NOTE: This could be prevented with a DontDestroyOnLoad
                    //	   if you really don't want this.
                    // No worries -- we'll just try the next one in our sequence.

                    return Spawn(pos, rot);
                }
            }

            active.Add(poolMember);

            poolMember.transform.position = pos;
            poolMember.transform.rotation = rot;

            poolMember.gameObject.SetActive(true);
            return poolMember;
        }

        // Return an object to the inactive pool.
        public void Despawn(PoolMember poolMember)
        {
            poolMember.gameObject.SetActive(false);

            if(active.Remove(poolMember))
                inactive.Push(poolMember);
        }
    }
}
