#!/usr/bin/env bash

if [ -z $1 ]; then
  echo "Need start directory"
  exit 1
fi

if [ -z $2 ]; then 
  echo "Need file extension"
  exit 1
fi

START=$(echo $1 | sed "s#//*#/#g")
EXT=$2

if [ ! -d $START ]; then
  echo "Start not a directory"
  exit 1
fi


shopt -s globstar
for n in $START/**/*.$EXT; do 
  echo "."$(echo $n | awk -F "/" '{print $NF}')
  echo ""[source,$EXT]""
  echo '----'
  echo "include::$n[]"; 
  echo '----'
  echo 
done

