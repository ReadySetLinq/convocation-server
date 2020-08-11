using System;
using XPression;

namespace ConvocationServer.XPN
{
    public class XPN_TakeItem
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public xpTakeItem Item { get; set; }

        public XPN_TakeItem(string name, int cID, xpTakeItem item)
        {
            Name = name;
            ID = cID;
            Item = item;
        }

        public int Layer
        {
            get
            {
                if (Item != null)
                    return Item.Layer;
                else
                    return -1;
            }
        }

        public bool IsOnline
        {
            get
            {
                if (Item != null)
                    return Item.IsOnline;
                else
                    return false;
            }
        }

        public bool Toggle()
        {
            if (Item != null)
            {
                if (Item.IsOnline)
                {
                    Item.Execute();
                    return true;
                } else
                {
                    Item.SetOffline();
                    return false;
                }
            }

            return false;
        }

        public bool SetOnline()
        {
            bool _success = false;
            if (Item != null)
                _success = Item.Execute();
            
            return _success;
        }

        public bool SetOffline()
        {
            bool _success = false;
            if (Item != null)
                _success = Item.SetOffline();

            return _success;
        }

        public void EditProperty(string objName, string propName, string value)
        {
            try
            {
                xpTakeItem takeItem = Item;
                if (takeItem != null && takeItem.GetPublishedObjectByName(objName, out xpPublishedObject publishedObject))
                {
                    int propCount = publishedObject.PropertyCount;
                    // Loop through all properties until we find the one with our selected name
                    for (int propID = 0; propID < propCount; propID++)
                    {
                        publishedObject.GetPropertyInfo(propID, out string tempName, out PropertyType propType);
                        // Check if name is what we are looking for
                        if (tempName.Equals(propName, StringComparison.OrdinalIgnoreCase))
                        {
                            switch (propType)
                            {
                                case PropertyType.pt_String:
                                    publishedObject.SetPropertyString(propID, value.Trim());
                                    break;
                                case PropertyType.pt_Boolean:
                                    Boolean val;
                                    if (Boolean.TryParse(value.Trim(), out val))
                                        publishedObject.SetPropertyBool(propID, val);
                                    break;
                                case PropertyType.pt_Material:
                                    int face = 0;
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        if (takeItem.Project.GetMaterialByName(value, out xpMaterial material))
                                            publishedObject.SetPropertyMaterial(propID, face, material);
                                    }
                                    break;
                            }
                        }
                    }
                    takeItem.UpdateThumbnail();
                }
            }
            catch { };
        }

    }
}
