using rak.being;
using rak.being.species;
using rak.being.species.critter;
using rak.equipment;
using System;
using System.Collections.Generic;
using System.IO;
using rak.unity.nonliving;
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
            string List = System.IO.File.ReadAllText("C:/Users/Public/Documents/Unity Projects/From The Beginning/Assets/Scripts/misc/Text/CritterLast.txt");
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
        public static GameObject FindClosest(string tag, Transform transform)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(tag);
            GameObject closest = null;
            float distance = Mathf.Infinity;
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
                    // Set claimed so we only have one agent at a time //
                    if (!go.GetComponent<RAKItem>().isClaimed())
                    {
                        valid = true;
                    }
                }
                // A valid target was found //
                if (valid)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        if(curDistance < .1)
                        {
                            Debug.Log("");
                        }
                        closest = go;
                        distance = curDistance;
                    }
                }
            }
            if(closest == null)
            {
                return null;
            }
            Debug.Log("Closest " + tag + " found");
            return closest;
        }
        public static GameObject FindClosestResource(Transform originTransform,ResourceType resourceType)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(Tags.TAG_RESOURCE);
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = originTransform.position;

            foreach (GameObject go in gos)
            {
                bool valid = false; // Did we find a valid object
                if(resourceType == ResourceType.FOOD)
                {
                    Resource resource = (Resource) go.GetComponent<RAKItem>().getItem();
                    if(resource.getResourceType() == ResourceType.FOOD)
                    {
                        valid = true;
                    }
                }
                // A valid target was found //
                if (valid)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        if (curDistance < .1)
                        {
                            Debug.Log("");
                        }
                        closest = go;
                        distance = curDistance;
                        Debug.Log("Closes object distance - " + curDistance);
                    }
                }
            }
            if (closest == null)
            {
                return null;
            }
            return closest;
        }
    }
}