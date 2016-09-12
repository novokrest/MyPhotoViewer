package dev.novokrest.myphotoviewer.model;

import com.google.common.collect.Lists;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;


public class PhotoYearsProvider {
    private final PhotosProvider photosProvider;

    public PhotoYearsProvider(PhotosProvider photosProvider) {
        this.photosProvider = photosProvider;
    }

    public Collection<Integer> getYears() {
        ArrayList<Photo> photos = Lists.newArrayList(photosProvider.getPhotos());
        List<Integer> years = photos.stream()
                .map(this::getPhotoCreationYear)
                .distinct().collect(Collectors.toList());
        years.sort(Integer::compare);
        return years;
    }

    private Integer getPhotoCreationYear(Photo photo) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(photo.getCreationDate());
        return calendar.get(Calendar.YEAR);
    }
}
