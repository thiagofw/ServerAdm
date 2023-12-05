#!/bin/bash
#########################################
# Simple add dep. into asp core project #
# 31/10/2023				#
#########################################
echo "Starting..."
clear
echo "Name of project"
read  project
echo "Installing EntityFrameworkCore.Design"
dotnet add $project package Microsoft.EntityFrameworkCore.Design
clear
echo "Installing EF tools"
dotnet add $project package Microsoft.EntityFrameworkCore.Tools
clear
echo "Installing MYSQL EF Version: 6.1"
dotnet add $project package MySql.EntityFrameworkCore -v 6.1
sleep 3
clear
echo "Into project dir"
cd $project 
echo "Creating Data folder..."
mkdir Data
sleep 2
echo "Creating Context..."
dotnet ef DbContext Scaffold "server=10.2.2.166;port=3306;user=vsuser;password=123456;database=vspro;" MySql.EntityFrameworkCore -o Data -f
echo "Done!"

