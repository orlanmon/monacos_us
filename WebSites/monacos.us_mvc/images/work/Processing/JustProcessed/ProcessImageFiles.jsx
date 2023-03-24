var InputFolder = ""
var OutputFolder = ""
var objInputFolder = null
var objSourceJPGFileListArray = null
var objTemplateThumbNailDocument = null
var objSourceJPGDocument = null
var jpgSaveOptions = null
var gifSaveForWebOptions = null
var psdSaveForWebOptions = null
var OutputFile = null
var objTempJPGDocument = null
var SourceHeight = 0
var SourceWidth = 0
var DestinationHeight = 0


// Notes:

// Final Thumbnail Image Landscape W: 210 H: 158
// Final Thumbnail Image Portrait W: 158 H: 210

// Thumbnail Image Foreground Layer  Landscape W: 200 H: 134
// Thumbnail Image Foreground Layer  Portrait W: 134 H: 200

// Final JPEG Dimentions Landscape W: 500 H: 375
// Final JPEG Dimentions Portrait  W: 375 H: 500




InputFolder = "C:/Software Development .NET/2.0/monacos.us/Projects/monacos.us/HomeSite/WebSites/monacos.us/images/Processing"

OutputFolder = "C:/Software Development .NET/2.0/monacos.us/Projects/monacos.us/HomeSite/WebSites/monacos.us/images/Processing/FinalImages"



objInputFolder = new Folder(InputFolder)




app.preferences.rulerUnits = Units.PIXELS
app.preferences.typeUnits = TypeUnits.PIXELS



objSourceJPGFileListArray = objInputFolder.getFiles("*.jpg")


// Close Anything That is Open
while( app.documents.length )
{
	app.activeDocument.close();
}

for ( var i=0; i < objSourceJPGFileListArray.length; i++ )
//for ( var i=0; i < 1; i++ )
{

	
	objSourceJPGDocument = open (objSourceJPGFileListArray[i]);
	

	if ( objSourceJPGDocument.height <= 1728 && objSourceJPGDocument.width >= 2304 )
	{
		// Landscape Processing 

        objTemplateThumbNailDocument = open( File(InputFolder + "/Landscape_tn.psd"))
       

		// Set Landscape Thumbnail Template To Active Document		
		app.activeDocument = objTemplateThumbNailDocument 


		app.activeDocument.activeLayer = app.activeDocument.artLayers.getByName("ForegroundImage")


		// Select Content of Foreground Image Layer		
		activeDocument.selection.selectAll()


		// Clear This Layer
		app.activeDocument.selection.clear()


		// Deselect Selection
		app.activeDocument.selection.deselect()


				
		// Set Active Doc to Source JPEG
		app.activeDocument = objSourceJPGDocument



		SourceHeight = objSourceJPGDocument.height
		SourceWidth = objSourceJPGDocument.width

		// Note 500px is the Destination Width of Finalized Picture JEPG
		// Final JPEG Dimentions Landscape W: 500 H: 375
				
		DestinationHeight = (( SourceHeight * 500 )/SourceWidth)



		// Now Create Finalized Picture JPEG		
		app.activeDocument.resizeImage(500, DestinationHeight, 230, ResampleMethod.BICUBIC )
		

		// Save Finalized Picture JPEG


		jpgSaveOptions = new JPEGSaveOptions() 
		jpgSaveOptions.format = SaveDocumentType.JPEG
		jpgSaveOptions.quality = 12
		jpgSaveOptions.matte = MatteType.NONE
		jpgSaveOptions.embedColorProfile = true

		

		OutputFile = new File (OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + ".jpeg" )

		//alert("Output File Reduced Size Jpeg: " + OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + ".jpeg" )

        // Finalized Picture JPEG Is Saved
		app.activeDocument.saveAs( OutputFile  , jpgSaveOptions, false,  Extension.LOWERCASE)


		// Now Preperation for Thumbnail from Current JPEG Document

		// Note 200px is the Destination Width of The Thumbnail Foreground Layer
		DestinationHeight = (( SourceHeight * 200 )/SourceWidth)


		// Resize It
		// app.activeDocument.resizeImage(200, 133, 230, ResampleMethod.BICUBICSHARPER )
		app.activeDocument.resizeImage(200, DestinationHeight, 230, ResampleMethod.BICUBICSHARPER )


		// Select Entire Document

		app.activeDocument.selection.selectAll()


		// Copy All Visible Layers Into Clipboard
		app.activeDocument.selection.copy()


		app.activeDocument.selection.deselect()


		// Set Landscape Thumbnail Template To Active Document		
		app.activeDocument = objTemplateThumbNailDocument 
		

		app.activeDocument.activeLayer = app.activeDocument.artLayers.getByName("ForegroundImage")


		// Paste JPEG Image Into Into Foreground Layer	Of Thumbnail	
		app.activeDocument.paste()


		// Save PSD Of Thumbnail
		// objSourceJPGDocument.name + "_tn"

		OutputFile = new File (OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.psd" )


		//alert("Output File PSD TN: " + OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.psd"  )

		psdSaveForWebOptions =  new PhotoshopSaveOptions()

		
		PhotoshopSaveOptions.layers = true


		app.activeDocument.saveAs( OutputFile, psdSaveForWebOptions,false ,Extension.LOWERCASE)



		// Save GIF of Thumbnail


		gifSaveForWebOptions = new ExportOptionsSaveForWeb()
		gifSaveForWebOptions.format = SaveDocumentType.COMPUSERVEGIF
		gifSaveForWebOptions.colors = 256
		gifSaveForWebOptions.dither = Dither.NONE
		gifSaveForWebOptions.quality = 0
		gifSaveForWebOptions.colorReduction = ColorReductionType.ADAPTIVE 
				
					
		OutputFile = new File (OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.gif" )
				

		//alert("Output File GIF TN: " + OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.gif" )


		
		app.activeDocument.exportDocument( OutputFile, ExportType.SAVEFORWEB, gifSaveForWebOptions )



		


	}
	else
	{
	
		// Portrait Processing 

        objTemplateThumbNailDocument = open( File(InputFolder + "/Portrait_tn.psd"))
       

		// Set Landscape Thumbnail Template To Active Document		
		app.activeDocument = objTemplateThumbNailDocument 


		app.activeDocument.activeLayer = app.activeDocument.artLayers.getByName("ForegroundImage")


		// Select Content of Foreground Image Layer		
		activeDocument.selection.selectAll()


		// Clear This Layer
		app.activeDocument.selection.clear()


		// Deselect Selection
		app.activeDocument.selection.deselect()
				
		// Set Active Doc to Source JPEG
		app.activeDocument = objSourceJPGDocument

		SourceHeight = objSourceJPGDocument.height
		SourceWidth = objSourceJPGDocument.width

		// Note 375px is the Destination Width of Finalized Picture JEPG
		// Final JPEG Dimentions Portrait  W: 375 H: 500

		
		DestinationHeight = (( SourceHeight * 375 )/SourceWidth)


		// Now Create Finalized Picture JPEG		
		app.activeDocument.resizeImage(375, DestinationHeight, 230, ResampleMethod.BICUBIC )
		

		// Save Finalized Picture JPEG


		jpgSaveOptions = new JPEGSaveOptions() 
		jpgSaveOptions.format = SaveDocumentType.JPEG
		jpgSaveOptions.quality = 12
		jpgSaveOptions.matte = MatteType.NONE
		jpgSaveOptions.embedColorProfile = true

		
		OutputFile = new File (OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + ".jpeg" )

		//alert("Output File Reduced Size Jpeg: " + OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + ".jpeg" )

        // Finalized Picture JPEG Is Saved
		app.activeDocument.saveAs( OutputFile  , jpgSaveOptions, false,  Extension.LOWERCASE)


		// Now Preperation for Thumbnail from Current JPEG Document

		// Note 134px is the Destination Width of The Thumbnail Foreground Layer
		DestinationHeight = (( SourceHeight * 134 )/SourceWidth)


		// Resize It
		// app.activeDocument.resizeImage(133, 200, 230, ResampleMethod.BICUBICSHARPER )
		app.activeDocument.resizeImage(134, DestinationHeight, 230, ResampleMethod.BICUBICSHARPER )


		// Select Entire Document

		app.activeDocument.selection.selectAll()


		// Copy All Visible Layers Into Clipboard
		app.activeDocument.selection.copy()


		app.activeDocument.selection.deselect()


		// Set Landscape Thumbnail Template To Active Document		
		app.activeDocument = objTemplateThumbNailDocument 
		

		app.activeDocument.activeLayer = app.activeDocument.artLayers.getByName("ForegroundImage")


		// Paste JPEG Image Into Into Foreground Layer	Of Thumbnail	
		app.activeDocument.paste()


		// Save PSD Of Thumbnail
		// objSourceJPGDocument.name + "_tn"

		OutputFile = new File (OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.psd" )


		//alert("Output File PSD TN: " + OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.psd"  )

		psdSaveForWebOptions =  new PhotoshopSaveOptions()

		
		PhotoshopSaveOptions.layers = true


		app.activeDocument.saveAs( OutputFile, psdSaveForWebOptions,false ,Extension.LOWERCASE)



		// Save GIF of Thumbnail


		gifSaveForWebOptions = new ExportOptionsSaveForWeb()
		gifSaveForWebOptions.format = SaveDocumentType.COMPUSERVEGIF
		gifSaveForWebOptions.colors = 256
		gifSaveForWebOptions.dither = Dither.NONE
		gifSaveForWebOptions.quality = 0
		gifSaveForWebOptions.colorReduction = ColorReductionType.ADAPTIVE 
				
					
		OutputFile = new File (OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.gif" )
				

		//alert("Output File GIF TN: " + OutputFolder + "/" + objSourceJPGDocument.name.slice(0,-4) + "_tn.gif" )


		
		app.activeDocument.exportDocument( OutputFile, ExportType.SAVEFORWEB, gifSaveForWebOptions )

	
	}


	
	
	
	objSourceJPGDocument.close( SaveOptions.DONOTSAVECHANGES )
	



}




objTemplateThumbNailDocument.close( SaveOptions.DONOTSAVECHANGES )



objInputFolder = null;
objOutputFolder = null;