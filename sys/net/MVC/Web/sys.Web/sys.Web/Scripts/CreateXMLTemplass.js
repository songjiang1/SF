function getXMLTemplass(option) {
    var xmlTemplass = new Object();
    if (option)
    {
        xmlTemplass.root = option.root;
        xmlTemplass.name = option.name;
        xmlTemplass.templeteName = option.templeteName; 
        xmlTemplass.ExSheets = option.ExSheets; 
    }
    return xmlTemplass;
}

