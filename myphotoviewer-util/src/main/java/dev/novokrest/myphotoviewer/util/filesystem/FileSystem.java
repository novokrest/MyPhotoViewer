package dev.novokrest.myphotoviewer.util.filesystem;


import dev.novokrest.myphotoviewer.util.core.Verifiers;

import java.io.File;
import java.nio.file.*;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

class FileSystem {

    private final String rootDirectoryPath;
    private final File rootDirectory;

    public FileSystem(String rootDirectoryPath) {
        Verifiers.Verify(!StringEx.isNullOrEmpty(rootDirectoryPath), "Empty folder path");
        File rootDirectory = new File(rootDirectoryPath);
        Verifiers.Verify(rootDirectory.isDirectory(), "Specified path is not directory");

        this.rootDirectoryPath = rootDirectoryPath;
        this.rootDirectory = rootDirectory;
    }

    public Iterator<String> listFilesInDirectory() {
        List<String> filePaths = new ArrayList<>();

        for (final File entry: rootDirectory.listFiles()) {
            if (entry.isFile()) {
                String fileName = entry.getName();
                Path filePath = Paths.get(rootDirectoryPath, fileName);
                filePaths.add(filePath.toString());
            }
        }

        return filePaths.iterator();
    }
}
