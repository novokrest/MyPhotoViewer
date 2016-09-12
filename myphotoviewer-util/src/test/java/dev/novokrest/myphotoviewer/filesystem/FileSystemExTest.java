package dev.novokrest.myphotoviewer.filesystem;


import com.google.common.collect.Lists;
import dev.novokrest.myphotoviewer.util.filesystem.FileSystemEx;
import org.junit.Test;

import java.nio.file.Paths;
import java.util.Iterator;
import java.util.List;

public class FileSystemExTest {

    @Test
    public void testListFilesInDirectory() {
        System.out.println(Paths.get("").toAbsolutePath().toString());
        final String photosDirectory = "../photos-test";
        Iterator<String> filesIterator = FileSystemEx.listFilesInDirectory(photosDirectory);
        printFiles(filesIterator);
    }

    private void printFiles(Iterator<String> filesIterator) {
        List<String> files = Lists.newArrayList(filesIterator);
        System.out.println(String.format("There are %s files", files.size()));
        files.forEach(System.out::println);
    }
}
