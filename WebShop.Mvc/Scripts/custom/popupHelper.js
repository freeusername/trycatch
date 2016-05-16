function showPopup(name, decription, priceExclVat, vat, priceInclVat) {
    $('#myModal')
        .find('div.content-name').text(name).end()
        .find('div.content-description').text(decription).end()
        .find('div.content-price-excl-vat').text(priceExclVat).end()
        .find('div.content-vat').text(vat).end()
        .find('div.content-price-incl-vat').text(priceInclVat).end()
        .modal();
}