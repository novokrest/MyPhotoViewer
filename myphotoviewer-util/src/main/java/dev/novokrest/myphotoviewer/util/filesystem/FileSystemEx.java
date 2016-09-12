package dev.novokrest.myphotoviewer.util.filesystem;


import com.google.common.collect.Iterators;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class FileSystemEx {

    public static Iterator<String> listFilesInDirectory(String directoryPath) {
        return new FileSystem(directoryPath).listFilesInDirectory();
    }

    public static Iterator<String> listFilesInDirectories(String... topLevelDirectories) {
        return Iterators.concat(getDirectoryIterators(topLevelDirectories));
    }

    private static Iterator<Iterator<String>> getDirectoryIterators(String... topLevelDirectories) {
        List<Iterator<String>> rootDirectoryIterators = new ArrayList<Iterator<String>>(topLevelDirectories.length);

        for(String rootDirectory : topLevelDirectories) {
            Iterator<String> filesIterator = FileSystemEx.listFilesInDirectory(rootDirectory);
            rootDirectoryIterators.add(filesIterator);
        }

        return rootDirectoryIterators.iterator();
    }
}
