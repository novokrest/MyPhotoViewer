package dev.novokrest.myphotoviewer.model;


import dev.novokrest.myphotoviewer.util.core.Verifiers;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.nio.file.attribute.BasicFileAttributes;
import java.util.Date;

public class Photo {
    private final String path;
    private final Date creationDate;

    public static Photo createFromFile(String path) throws IOException {
        File file = new File(path);
        Verifiers.Verify(file.isFile(), "Specified path doesn't point to file");

        BasicFileAttributes fileAttributes = Files.readAttributes(Paths.get(path), BasicFileAttributes.class);
        Date fileCreationDate = getFileCreationDate(fileAttributes);

        return new Photo(path, fileCreationDate);
    }

    private static Date getFileCreationDate(BasicFileAttributes fileAttributes) {
        long fileCreationMs = Math.min(fileAttributes.creationTime().toMillis(),
                                       fileAttributes.lastModifiedTime().toMillis());
        return new Date(fileCreationMs);
    }

    public Photo(String path, Date creationDate) {
        this.path = path;
        this.creationDate = creationDate;
    }

    public String getPath() {
        return path;
    }

    public Date getCreationDate() {
        return creationDate;
    }

    @Override
    public boolean equals(Object other) {
        if (other == null) return false;
        if (!(other instanceof Photo)) return false;

        Photo otherPhoto = (Photo) other;
        return path.equals(otherPhoto.path) && creationDate.equals(otherPhoto.creationDate);
    }

    @Override
    public String toString() {
        return String.format("[%s, Created:%s]", path, creationDate);
    }
}
