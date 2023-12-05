File naming:

FormMain						//Main Form
FormGameSelect					//Game Selection Form

FuncFormMain					//Functions for updating parts of the main form

FuncSaveReader					//Main function for reading Save Files / Battle Video Files and properly extracting their data
FuncBattleVideoPokemonParser	//Main function for reading Pokemon data from decrypted Battle Videos

FuncPokemonSprites				//Functions related to pokemon sprites
FuncTextParser					//Main Function used for converting byte-text to actual strings

ClassBattleVideoPokemon			//Class file for Battle Video Pokemon along with needed methods
ClassBoxUploadPokemon*			//Class file for Box Upload Pokemon



Program Process:

Step 1: Reading from the save file
	-Open the save file
	-Prompt which game it's for
	-Locate and read the encrypted video data (stored in encrypted buffer)
	-Copy encrypted video data and decrypt it (stored in decrypted buffer)



Step 2: Parsing Pokemon data from a decrypted battle video
	-
