 @ECHO OFF    
SET comment=Saved on %date%-%time%
IF "%~1"=="" GOTO COMMIT
SET username="Vadim001"
SET comment=%comment% by %username%
 
:COMMIT
ECHO %comment%
 
rem git checkout main
git status
git add *
                  
git commit -m "%comment%"
git push
