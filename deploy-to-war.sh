#!/bin/bash

TOMCAT=../tomcat
TOMCAT_WEBAPPS=$TOMCAT/webapps
WAR=myphotoviewer.war

jar -cvf $WAR WEB-INF classes index.html
cp $WAR $TOMCAT_WEBAPPS
rm -f $WAR
echo Done