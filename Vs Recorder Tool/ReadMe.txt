File naming:

FormMain						//Main Form
FormGameSelect					//Game Selection Form

FuncFormMain					//Logical functions for updating parts of the main form
GuiFormMain						//Graphical functions for updating parts of the main form

FuncSaveReader					//Main function for reading Save Files / Battle Video Files and properly extracting their data
FuncBattleVideoPokemonParser	//Main function for reading Pokemon data from decrypted Battle Videos

FuncPokemonSprites				//Functions related to pokemon sprites
FuncTextParser					//Main Function used for converting byte-text to actual strings

ClassBattleVideoPokemon			//Class file for Battle Video Pokemon along with needed methods
ClassBoxUploadPokemon*			//Class file for Box Upload Pokemon


====Functional breakdown====
OpenUserFile - Promts the user to select a file and ask which game the file belongs to in case of a save file
inputs:
outputs: FilePath(global), CurrentGame(global)

ExportBattleVideo - Prompts the user to select a location on their pc and saves the currently selected battle video there

ImportBattleVideo - Prompts the user to select a battle video file and then puts it in the video arrays as well as writing it to the currently selected save file

==File Readers==
ReadFromSaveFile4 - Reads battle video data from a gen 4 save file and stores them in the encrypted video array
inputs: FilePath(global), CurrentGame(global)
output: EncrpytedArray(global)


ReadFromSaveFile5 - Reads battle video data from a gen 5 save file and stores them in the encrypted video array
inputs: FilePath(global), CurrentGame(global)
output: EncrpytedArray(global)


ReadFromBattleVideoFile - Reads battle video data from a bv file and stores it into slot0 of the target array (also wipes the other slots)
input: FilePath(global), TargetArray
output: TargetArray


==Cryption==
DecryptBattleVideos - Decrypt the contents of the encrypted array according to the bitmask and store the results in the decrypted array
inputs: EncryptedArray(global), Bitmask
outputs: DecryptedArray(global)


==Pokemon Parsing==
OpenBattleVideoPokemonFile - Converts the pokemon data in the decrypted array into a human-readable pokemon[] 

FrontSprite - returns the front sprite of a given pokemon
MenuSprite - returns the menu sprite of a given pokemon
HoldItemSprite - returns the hold item icon of a given pokemon
PokeballSprite - returns the pokeball icon of a given pokemon

TextParser - converts a byte[] of text data into the corresponding string
inputs: NameData[], CurrentGame(global)
outputs: Name

==Gui Updates==
UpdateBattleVideoSlots - Updates the radio buttons to match the state of the battle videos within the decrypted array

ClearPreviewPokemon - Clears the contents of the preview pokemon slots
UpdatePreviewPokemon - Updates the contents of the preview pokemon slots with the pokemon stored in the pokemon[]

SetCurrentPokemon - Updates the contents of the pokemon panels with the data of the specified pokemon in the pokemon[]



Program Process:

Step 1: Reading from a file and fill the video arrays accordingly
	//FileHandlers - OpenUserFile
	-Open the save file
	-Prompt which game it's for

	//SaveReader - *
	-Locate and read the encrypted video data (stored in encrypted buffer)
	-Copy encrypted video data and decrypt it (stored in decrypted buffer)



Step 2: Parsing Pokemon data from a decrypted battle video
	-
