import os
from shutil import copy, rmtree, copytree

mod_directory = r"C:\Users\Think\AppData\Roaming\r2modmanPlus-local\ULTRAKILL\profiles\default-ultrakill\BepInEx\plugins\Einfachirgendwa1-UltrakillMod"

print("Compiling project")
os.system("dotnet build")

copy("obj/Debug/netstandard2.1/UltrakillMod.dll", "Result/UltrakillMod/UltrakillMod.dll")
print("Deleting old directory")
rmtree(mod_directory, ignore_errors=True)
print("Cloning new content")
copytree("Result", mod_directory)