package dev.novokrest.myphotoviewer.model;


import java.util.Date;

public class Photo {
    private final String title;
    private final Date date;

    public Photo(String title, Date date) {
        this.title = title;
        this.date = date;
    }

    public String getTitle() {
        return title;
    }

    public Date getDate() {
        return date;
    }
}
