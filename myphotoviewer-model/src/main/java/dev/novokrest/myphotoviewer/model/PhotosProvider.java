package dev.novokrest.myphotoviewer.model;


import com.google.common.collect.Iterators;
import dev.novokrest.myphotoviewer.util.filesystem.FileSystemEx;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;


public class PhotosProvider {
    private final List<String> rootDirectories;

    public PhotosProvider(String... rootDirectories) {
        this.rootDirectories = Arrays.asList(rootDirectories);
    }

    public Iterator<String> getRootPhotos() {
        return Iterators.concat(getRootDirectoryIterators());
    }

    private Iterator<Iterator<String>> getRootDirectoryIterators() {
        List<Iterator<String>> rootDirectoryIterators = new ArrayList<Iterator<String>>(rootDirectories.size());

        for(String rootDirectory : rootDirectories) {
            Iterator<String> filesIterator = FileSystemEx.listFilesInDirectory(rootDirectory);
            rootDirectoryIterators.add(filesIterator);
        }

        return rootDirectoryIterators.iterator();
    }
}
