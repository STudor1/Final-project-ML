This is what has worked for my workspace, this might differ on different computers.

------------------------------------------------------------
Versions
------------------------------------------------------------
Unity version: 2021.3.18f1
Python version: 3.9
ML Agent version: 0.30.0
Pytorch version: 1.7.1

------------------------------------------------------------
Set up
------------------------------------------------------------
First thing to do is to install python. After this is done set up a virtual environment in the project folder by following this
tutorial: 
https://www.youtube.com/watch?v=zPFU30tbyKs
or by checking the official ml agents page:
https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Using-Virtual-Environment.md

After a virtual environment has been created open it up and follow the steps from:
https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Installation.md

Open up unity and install the ml agents package, this might get some errors that you can ignore.
To check that everything has installed correctly run the following command in command prompt while in the project folder
venv\Scripts\activate
This now starts the virtual environment. Run this to check ml agents has installed correctly
mlagents-learn --help
If any errors occur the set up is not correct or versions might differ. 

------------------------------------------------------------
Commands to start the training
------------------------------------------------------------ 
To start training use the following command
mlagents-learn --run-id=NameOfTest 
Replace NameOfTest to what you want the test to be called. This will run the training with the default configuration.

To change the configuration first run a training session with the default one. This will then create a results folder
with all the tests and the brains created. Open this folder and get a copy of the configuration.yaml file and place it in the 
project folder, outside of the results folder. 
To run a training session with the new configuration file, or the file provided use

mlagents-learn .\configuration.yaml --run-id=NameOfTest 

If any errors occur when running these commands add -- force at the end of the command, this will force the algorithm
to override the brain. 

------------------------------------------------------------
TestEnvironments
------------------------------------------------------------
Test Env 1/2 work with GeneticAlgoRays and AgentsManager use the brain from TestEnv1.01 to showcase test env 1
Test Env 1.1 works with AgentRL use the brain from TestEnv1.10 to showcase test env 1.1
Test Env 3.1 works with AgentRL use the brain from TestEnv3.10 to showcase test env 3.1 

------------------------------------------------------------
Run the project
------------------------------------------------------------
To run the project open start menu, then connect to the trainer with the steps above. Create a maze and let the agent train on the
custom maze, this could take a few hours depending on the maze difficulty. 
