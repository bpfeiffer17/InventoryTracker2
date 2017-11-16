public AssetBare getAssetBare()
{
    AssetBare assetBare = new AssetBare();
    assetBare.AssetID = this.AssetID;
    assetBare.AssetType = this.AssetType.getAssetTypeBare();
    // Set the values for the properties 
    var i = 0;
    foreach (Property prop in this.AssetType.Properties)
    {
        assetBare.AssetType.Properties[i].Value = this.getPropertyValue(prop);
        i++;
    }
    return assetBare;
}

private string getPropertyValue(Property prop)
{
    foreach (PropertyValue propValue in this.PropertyValues)
    {
        if (propValue.PropertyID == prop.PropertyID)
        {
            return propValue.Value;
        }
    }
    return "";
} 
 
 
    public class AssetBare
{
    public int AssetID { get; set; }
    public AssetTypeBare AssetType { get; set; }
}