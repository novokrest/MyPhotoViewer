function loadImage() {
    return getImg();
}

function getImg() {
    return $('div.templates img.thumbnail').clone();
}

function getMockImg() {
    var $mockImgTemplates = $('div.templates .mock-img');
    var mockImgTemplatesTotal = $mockImgTemplates.length;
    var mockImgTemplateNumber = getRandomInt(0, mockImgTemplatesTotal);

    return $($mockImgTemplates.get(mockImgTemplateNumber)).clone();
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}

var images = $.map(Array(10), loadImage);
var resizedTable = new ResizedTable(images, $('.photo-album'));