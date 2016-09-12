#!/bin/bash

TOMCAT=../tomcat
TOMCAT_BIN=$TOMCAT/bin
TOMCAT_WEBAPPS=$TOMCAT/webapps
WAR=myphotoviewer-web/target/myphotoviewer-web.war
TOMCAT_WAR_DIR=$TOMCAT_WEBAPPS/myphotoviewer
TOMCAT_WAR=$TOMCAT_WEBAPPS/myphotoviewer.war

rm -rf myphotoviewer-web/target/myphotoviewer
rm -rf myphotoviewer-web/target/myphotoviewer
rm -f myphotoviewer-web/target/myphotoviewer.web
mvn clean
mvn package

$TOMCAT_BIN/shutdown.sh
sleep 1s
echo Deleting $TOMCAT_WAR_DIR...
rm -rf $TOMCAT_WAR_DIR
rm -rf $TOMCAT_WAR_DIR
rm -f $TOMCAT_WAR

$TOMCAT_BIN/catalina.sh jpda start
sleep 5s
cp $WAR $TOMCAT_WAR
echo WAR deployed successfully

echo tomcat has been restarted

echo Done