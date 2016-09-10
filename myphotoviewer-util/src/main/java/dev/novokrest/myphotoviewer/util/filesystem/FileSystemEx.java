package dev.novokrest.myphotoviewer.util.filesystem;


import java.util.Iterator;

public class FileSystemEx {

    public static Iterator<String> listFilesInDirectory(String directoryPath) {
        return new FileSystem(directoryPath).listFilesInDirectory();
    }
}
