using rak.being;
using rak.being.species;
using rak.being.species.critter;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace rak.util
{
    public class Util
    {
        public static String[] critterLast = parseList();

        public static string getRandomString(string baseString)
        {
            return baseString + "-" + new System.Random().Next();
        }

        public static string numbersAfterDecimal(double bigNumber, int index)
        {
            if (bigNumber.ToString().IndexOf(".") > -1)
            {
                return bigNumber.ToString().Substring(0, bigNumber.ToString().IndexOf(".") + index);
            } else
            {
                return bigNumber.ToString();
            }
        }

        private static string[] parseList()
        {
            string List = System.IO.File.ReadAllText("C:/Users/Public/Documents/Unity Projects/From The Beginning/Assets/misc/Text/CritterLast.txt");
            List<string> arrayList = new List<string>();
            foreach (string entry in List.Split(' '))
            {
                arrayList.Add(entry);
            }
            return arrayList.ToArray();
        }

        public static string getLastName(Being being)
        {
            if(being.GetType() == typeof(Critter))
            {
                return critterLast[new System.Random().Next(0,critterLast.Length)];
            }
            return "None";
        }

        // Find closes object based on tag //
        public static GameObject FindClosest(string tag,Transform transform)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(tag);
            GameObject closest = null;
            float distance = Mathf.Infinity; // Any distance
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                bool valid = false; // Did we find a valid object
                if (Tags.TAG_BASE.Equals(tag)) // Is a base
                {
                    valid = true;
                }
                else if (Tags.TAG_RESOURCE.Equals(tag)) // Is a resource
                {
                    // Check that the resource is in a state that it can be picked up //
                    MeshRenderer renderer = go.GetComponent<MeshRenderer>();
                    if (renderer.enabled)
                    {
                        // Set claimed so we only have one agent at a time //
                        if (!go.GetComponent<RAKResource>().isClaimed())
                        {
                            valid = true;
                        }
                    }
                }
                // A valid target was found //
                if (valid)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
                // Could not find valid target for agent //
                else
                {
                    //Debug.Log(gameObject.name + " can't find target with tag " + tag);
                }
            }
            return closest;
        }
    }
}