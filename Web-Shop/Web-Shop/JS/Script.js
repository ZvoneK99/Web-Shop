
jQuery(document).ready(function () {

    // Polyfill za jQuery live funkciju
    if (typeof jQuery.fn.live == 'undefined' || !(jQuery.isFunction(jQuery.fn.live))) {
        jQuery.fn.extend({
            live: function (event, callback) {
                if (this.selector) {
                    jQuery(document).on(event, this.selector, callback);
                }
            }
        });
    }

    // Funkcija za odgodu izvršavanja (debounce)
    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
    //U kosarici brisanje artikla i mjenjanje kolicine
    var kosarica = {};
    kosarica.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".tjelo.kosarica");
        //Primjeni na sve komponente
        komponente.each(function () {
            var komponenta, tablica, kolicinaKosarice, btnMinus, btnPlus, btnBrisiArtikal, btnPosalji;
            komponenta = $(this);
            tablica = komponenta.find(".table.stavke .tbStavkelBody");
            kolicinaKosarice = komponenta.find(".input-text.qty");
            btnBrisiArtikal = komponenta.find(".cart-remove-item");
            var top = document.getElementById("breadcrumbs");

            btnBrisiArtikal.on("click", function () {
                var polje = $(this);
                var kolicina = "-1"
                var id = polje.data("id")
                var rb = polje.parents(".product-row").eq(0).data("rb");
                var trenutna = "1";
                var tabela = komponenta.find(".tabelaRefresh");
                $.post("/Ajax/ArtikliDodajKolicinu.aspx", {
                    id: id,
                    rb: rb,
                    kolicina: kolicina,
                    trenutna: trenutna
                }, function (podaci) {
                    location.reload();
                    //}
                });
            });


            kolicinaKosarice.on("keyup change", function () {
                var polje = $(this);
                var kolicina = parseInt(polje.val(), 10);
                if (isNaN(kolicina) || kolicina < 0) return;

                var row = polje.closest(".product-row");
                var id = row.data("id");
                var jedinicna = parseFloat(row.data("jedcijena"));
                var ukupnoPolje = row.find(".ukupna-cijena");

                $.post("/Ajax/ArtikliPromjeniKolicinu.aspx", {
                    id: id,
                    kolicina: kolicina
                }, function (podaci) {
                    // Lokalno izračunaj novu cijenu i prikaži bez reloadanja stranice
                    var novaCijena = (kolicina * jedinicna).toFixed(2);
                    ukupnoPolje.text(novaCijena + " KM"); // ili druga valuta
                    location.reload();
                });
            });


            kolicinaKosarice.on("change", function () {
                var polje = $(this);
                var kolicina = polje.val();
                var id = polje.parents(".product-row").eq(0).data("id");
                var rb = polje.parents(".product-row").eq(0).data("rb");
                var trenutna = polje.parents(".product-row").eq(0).data("trenutna");
                delay(function () {
                    $.post("/Ajax/ArtikliPromjeniKolicinu.aspx", {
                        id: id,
                        rb: rb,
                        kolicina: kolicina,
                        trenutna: trenutna
                    }, function (podaci) {
                        location.reload();
                    });
                }, 1);
            });
        });
    };
    kosarica.shop();

    //U kosarici adresa i dostava
    var kosaricaAdresa = {};
    kosaricaAdresa.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".shopArdesa");
        //Primjeni na sve komponente
        komponente.each(function () {
            var komponenta, btnPlatiPouzecem;
            komponenta = $(this);
            btnPlatiPouzecem = komponenta.find(".button.btn-proceed-platigotovinski");
            btnPlatiVirmanom = komponenta.find(".button.btn-proceed-plativirman");
            btnPlatiKarticom = komponenta.find(".button.btn-proceed-platikartice");
            btnPlatiKarticomRba = komponenta.find(".button.btn-proceed-platikarticeRba");
            btnPlatiMikrofin = komponenta.find(".button.btn-proceed-mikrofin");
            ddlBrojRata = komponenta.find(".ddlBrojRata");
            var top = document.getElementById("breadcrumbs");

            btnPlatiPouzecem.live("click", function () {
                var dugmic, maska, ime, adresa, mail, grad, zip, telefon, napomena, nacindostave;
                dugmic = $(this);
                ime = $(".txtIme").val();
                adresa = $(".txtAdresa").val();
                grad = $(".txtGrad").val();
                zip = $(".txtZIP").val();
                mail = $(".txtEmail").val();
                telefon = $(".txtTelefon").val();
                napomena = $(".txtNapomena").val();
                nacindostave = komponenta.find(".ddlNacinDostave").val();

                if (ime.length < 1) {
                    window.alert("Ime i prezime su obavezni");
                    return false;
                }
                if (adresa.length < 1) {
                    window.alert("Adresa je obavezna");
                    return false;
                }
                if (grad.length < 1) {
                    window.alert("Grad je obavezan");
                    return false;
                }
                if (zip.length < 1) {
                    window.alert("Poštanski broj je obavezan");
                    return false;
                }
                if (mail.length < 1) {
                    window.alert("Mail je obavezan");
                    return false;
                }
                if (telefon.length < 1) {
                    window.alert("Telefon je obavezan");
                    return false;
                }

                if (nacindostave == 0) {
                    document.getElementById("ddlNacinDostave").style.borderColor = "#FF0000";
                    return false;
                } else {
                    if (confirm('Želite li poslati narudžbu?!')) {
                        maska = komponenta.find(".maska");
                        document.getElementById("maska").style.display = 'block';
                        $.post("/Ajax/PopuniKupcaSession.aspx", {
                            ime: ime,
                            adresa: adresa,
                            grad: grad,
                            zip: zip,
                            mail: mail,
                            telefon: telefon
                        }, function (redci) {
                            $.post("/Ajax/PosaljiNarudzbuSession.aspx", {
                                nacin: "pouzece",
                                napomena: napomena,
                                nacindostave: nacindostave
                            }, function (redci) {
                                setTimeout("location.href = '/';", 100);
                            });
                        });
                    }
                }

            });

            btnPlatiVirmanom.live("click", function () {
                var dugmic, maska, ime, adresa, mail, grad, zip, telefon, napomena, nacindostave;
                dugmic = $(this);
                ime = $(".txtIme").val();
                adresa = $(".txtAdresa").val();
                grad = $(".txtGrad").val();
                zip = $(".txtZIP").val();
                mail = $(".txtEmail").val();
                telefon = $(".txtTelefon").val();
                napomena = $(".txtNapomena").val();
                nacindostave = komponenta.find(".ddlNacinDostave").val();

                if (ime.length < 1) {
                    window.alert("Ime i prezime su obavezni");
                    return false;
                }
                if (adresa.length < 1) {
                    window.alert("Adresa je obavezna");
                    return false;
                }
                if (grad.length < 1) {
                    window.alert("Grad je obavezan");
                    return false;
                }
                if (zip.length < 1) {
                    window.alert("Poštanski broj je obavezan");
                    return false;
                }
                if (mail.length < 1) {
                    window.alert("Mail je obavezan");
                    return false;
                }
                if (telefon.length < 1) {
                    window.alert("Telefon je obavezan");
                    return false;
                }

                if (nacindostave == 0) {
                    document.getElementById("ddlNacinDostave").style.borderColor = "#FF0000";
                    return false;
                } else {
                    if (confirm('Želite li poslati narudžbu?!')) {
                        maska = komponenta.find(".maska");
                        document.getElementById("maska").style.display = 'block';
                        $.post("/Ajax/PopuniKupcaSession.aspx", {
                            ime: ime,
                            adresa: adresa,
                            grad: grad,
                            zip: zip,
                            mail: mail,
                            telefon: telefon
                        }, function (redci) {
                            $.post("/Ajax/PosaljiNarudzbuSession.aspx", {
                                nacin: "virman",
                                napomena: napomena,
                                nacindostave: nacindostave
                            }, function (redci) {
                                setTimeout("location.href = '/';", 100);
                            });
                        });
                    }
                }


            });

            btnPlatiKarticom.live("click", function () {
                var dugmic, maska, rate, ime, adresa, mail, grad, zip, telefon, napomena, nacindostave;
                dugmic = $(this);
                maska = komponenta.find(".maska");
                rate = komponenta.find(".ddlBrojRata").val();
                ime = $(".txtIme").val();
                adresa = $(".txtAdresa").val();
                grad = $(".txtGrad").val();
                zip = $(".txtZIP").val();
                mail = $(".txtEmail").val();
                telefon = $(".txtTelefon").val();
                napomena = $(".txtNapomena").val();
                nacindostave = komponenta.find(".ddlNacinDostave").val();

                if (ime.length < 1) {
                    window.alert("Ime i prezime su obavezni");
                    return false;
                }
                if (adresa.length < 1) {
                    window.alert("Adresa je obavezna");
                    return false;
                }
                if (grad.length < 1) {
                    window.alert("Grad je obavezan");
                    return false;
                }
                if (zip.length < 1) {
                    window.alert("Poštanski broj je obavezan");
                    return false;
                }
                if (mail.length < 1) {
                    window.alert("Mail je obavezan");
                    return false;
                }
                if (telefon.length < 1) {
                    window.alert("Telefon je obavezan");
                    return false;
                }

                if (rate == 0) {
                    document.getElementById("ddlBrojRata").style.borderColor = "#FF0000";
                    return false;
                } else {
                    if (nacindostave == 0) {
                        document.getElementById("ddlNacinDostave").style.borderColor = "#FF0000";
                        return false;
                    } else {
                        $.post("/Ajax/PopuniKupcaSession.aspx", {
                            ime: ime,
                            adresa: adresa,
                            grad: grad,
                            zip: zip,
                            mail: mail,
                            telefon: telefon
                        }, function (redci) {
                            window.location = "/Ajax/SessionPikPayPlacanje.aspx?nacin=MonriPayten&rata=" + rate + "&napomena=" + napomena + "" + "&nacindostave=" + nacindostave + "";
                        });
                    }
                }

            });

            btnPlatiKarticomRba.live("click", function () {
                var dugmic, maska, rate, ime, adresa, mail, grad, zip, telefon;
                dugmic = $(this);
                maska = komponenta.find(".maska");
                rate = komponenta.find(".ddlBrojRata").val();
                ime = $(".txtIme").val();
                adresa = $(".txtAdresa").val();
                grad = $(".txtGrad").val();
                zip = $(".txtZIP").val();
                mail = $(".txtEmail").val();
                telefon = $(".txtTelefon").val();

                if (ime.length < 1) {
                    window.alert("Ime i prezime su obavezni");
                    return false;
                }
                if (adresa.length < 1) {
                    window.alert("Adresa je obavezna");
                    return false;
                }
                if (grad.length < 1) {
                    window.alert("Grad je obavezan");
                    return false;
                }
                if (zip.length < 1) {
                    window.alert("Poštanski broj je obavezan");
                    return false;
                }
                if (mail.length < 1) {
                    window.alert("Mail je obavezan");
                    return false;
                }
                if (telefon.length < 1) {
                    window.alert("Telefon je obavezan");
                    return false;
                }

                if (rate == 0) {
                    document.getElementById("ddlBrojRata").style.borderColor = "#FF0000";
                    return false;
                } else {
                    $.post("/Ajax/PopuniKupcaSession.aspx", {
                        ime: ime,
                        adresa: adresa,
                        grad: grad,
                        zip: zip,
                        mail: mail,
                        telefon: telefon
                    }, function (redci) {
                        window.location = "/EpayGo.aspx?nacin=ePay&rata=" + rate;
                    });
                }

            });

            ddlBrojRata.change(function () {
                var ime, adresa, mail, grad, zip, telefon;
                ime = $(".txtIme").val();
                adresa = $(".txtAdresa").val();
                grad = $(".txtGrad").val();
                zip = $(".txtZIP").val();
                mail = $(".txtEmail").val();
                telefon = $(".txtTelefon").val();
                napomena = $(".txtNapomena").val();
                $.post("/Ajax/PopuniKupcaSession.aspx", {
                    ime: ime,
                    adresa: adresa,
                    grad: grad,
                    zip: zip,
                    mail: mail,
                    telefon: telefon,
                    napomena: napomena
                }, function (redci) {
                    window.location = "/adresa-dostave?nacin-placanja=kartice&rata=" + ddlBrojRata.val() + "#kosarica";
                });
            });

            btnPlatiMikrofin.live("click", function () {
                var dugmic, maska, ime, adresa, mail, grad, zip, telefon, napomena, nacindostave;
                dugmic = $(this);
                ime = $(".txtIme").val();
                adresa = $(".txtAdresa").val();
                grad = $(".txtGrad").val();
                zip = $(".txtZIP").val();
                mail = $(".txtEmail").val();
                telefon = $(".txtTelefon").val();
                napomena = $(".txtNapomena").val();
                nacindostave = komponenta.find(".ddlNacinDostave").val();

                if (ime.length < 1) {
                    window.alert("Ime i prezime su obavezni");
                    return false;
                }
                if (adresa.length < 1) {
                    window.alert("Adresa je obavezna");
                    return false;
                }
                if (grad.length < 1) {
                    window.alert("Grad je obavezan");
                    return false;
                }
                if (zip.length < 1) {
                    window.alert("Poštanski broj je obavezan");
                    return false;
                }
                if (mail.length < 1) {
                    window.alert("Mail je obavezan");
                    return false;
                }
                if (telefon.length < 1) {
                    window.alert("Telefon je obavezan");
                    return false;
                }

                if (nacindostave == 0) {
                    document.getElementById("ddlNacinDostave").style.borderColor = "#FF0000";
                    return false;
                } else {
                    if (confirm('Želite li poslati narudžbu?!')) {
                        maska = komponenta.find(".maska");
                        document.getElementById("maska").style.display = 'block';
                        $.post("/Ajax/PopuniKupcaSession.aspx", {
                            ime: ime,
                            adresa: adresa,
                            grad: grad,
                            zip: zip,
                            mail: mail,
                            telefon: telefon
                        }, function (redci) {
                            $.post("/Ajax/PosaljiNarudzbuSession.aspx", {
                                nacin: "mikrofin",
                                napomena: napomena,
                                nacindostave: nacindostave
                            }, function (redci) {
                                setTimeout("location.href = '/';", 100);
                            });
                        });
                    }
                }

            });

        });
    };
    kosaricaAdresa.shop();

    /*----------------Funkcija da se azurira kolicina na ikonici kosarice u header----------------*/
    var kosaricaShop = {};
    kosaricaShop.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".shopArt");
        //Primjeni na sve komponente
        komponente.each(function () {
            var komponenta, dugmicDodaj1, tablica;
            komponenta = $(this);
            dugmicDodaj1 = komponenta.find(".dugmicDodaj");
            tablica = komponenta.find(".products-grid");
            var top = document.getElementById("breadcrumbs");
            //Dodaj u kosaricu
            dugmicDodaj1.click(function () {
                var dugmic, redak, id;
                dugmic = $(this);
                id = dugmic.data("id");
                redak = dugmic.parents(".div" + id).eq(0);
                var kolicina = 1;
                kolicina = redak.find(".qty." + id).val();
                trenutnoKosarica = $(".cart-count.spnKol");
                $.post("/Ajax/ArtikliDodajStavku.aspx", {
                    id: id,
                    kolicina: kolicina
                }, function (podaci) {
                    if (podaci.length > 0) {
                        trenutnoKosarica.html(podaci);
                    }
                });
            });
        });
    };
    kosaricaShop.shop();

    //Prikaz artikala na kategoriji i podkategoriji
    var kosaricaKategorija = {};
    kosaricaKategorija.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".shop-inner.kategorija");
        //Primjeni na sve komponente
        komponente.each(function () {
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, sort;
            komponenta = $(this);
            tablica = komponenta.find(".products-grid");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            poljeKategorija = komponenta.find(".hidKategorijaID");
            sort = komponenta.find(".ddlSortiraj");
            var top = document.getElementById("breadcrumbs");

            prikazi = function (pomak, raspored) {
                //Definiraj varijable
                var stranica, podkategorija;

                //Uzmi podatke iz polja
                stranica = parseInt(poljeStranica.val(), 10);
                kategorija = parseInt(poljeKategorija.val(), 10);
                raspored = sort.val();

                //Provjera je li proslijeđen pomak
                if (stranica <= 0) { stranica = 1; }
                if (pomak) { stranica = stranica + pomak; }
                if (!pomak) { stranica = 1; }

                //Provjeri ograničenje stranica
                if (stranica > 0) {
                    $.get("/Ajax/ArtikliNadGrupe.aspx", {
                        stranica: stranica,
                        kategorija: kategorija,
                        raspored: raspored
                    }, function (podaci) {
                        if ((pomak && podaci) || !pomak) {
                            tablica.html(podaci);
                            poljeStranica.val(stranica);
                            document.getElementById('breadcrumbs').scrollIntoView({
                                block: "start",
                                behavior: "smooth"
                            });
                        }
                    });
                }
            };

            dugmicSlijedeca.click(function () {
                prikazi(+1);
            });

            dugmicPrethodna.click(function () {
                prikazi(-1);
            });


            sort.change(function () {
                prikazi(0, sort.val());
            });
        });
    };
    kosaricaKategorija.shop();

    //Prikaz artikala na podkategoriji
    var kosaricaPodKategorija = {};
    kosaricaPodKategorija.shop = function () {
        var komponente = $(".shop-inner.podkategorija");
        komponente.each(function () {
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, sort;
            komponenta = $(this);
            tablica = komponenta.find(".products-grid");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            poljeKategorija = komponenta.find(".hidPodKategorijaID");
            sort = komponenta.find(".ddlSortiraj");
            var top = document.getElementById("breadcrumbs");

            prikazi = function (pomak, raspored) {
                //Definiraj varijable
                var stranica, podkategorija;

                //Uzmi podatke iz polja
                stranica = parseInt(poljeStranica.val(), 10);
                kategorija = parseInt(poljeKategorija.val(), 10);
                raspored = sort.val();

                //Provjera je li proslijeđen pomak
                if (stranica <= 0) { stranica = 1; }
                if (pomak) { stranica = stranica + pomak; }
                if (!pomak) { stranica = 1; }

                //Provjeri ograničenje stranica
                if (stranica > 0) {
                    $.get("/Ajax/ArtikliGrupeTablica.aspx", {
                        stranica: stranica,
                        kategorija: kategorija,
                        raspored: raspored
                    }, function (podaci) {
                        if ((pomak && podaci) || !pomak) {
                            tablica.html(podaci);
                            poljeStranica.val(stranica);
                            document.getElementById('breadcrumb').scrollIntoView({
                                block: "start",
                                behavior: "smooth"
                            });
                        }
                    });
                }
            };

            //Navigacija slijedeæi artikli
            dugmicSlijedeca.click(function () {
                prikazi(+1);
            });

            //Navigacija prethodni artikli
            dugmicPrethodna.click(function () {
                prikazi(-1);
            });

            sort.change(function () {
                prikazi(0, sort.val());
            });
        });
    };
    kosaricaPodKategorija.shop();

    var blog = {};
    blog.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".blog_post");
        //Primjeni na sve komponente
        komponente.each(function () {
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna;
            komponenta = $(this);
            tablica = komponenta.find(".blog-posts");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            var top = document.getElementById("blog-posts");

            prikazi = function (pomak) {
                //Definiraj varijable
                var stranica;

                //Uzmi podatke iz polja
                stranica = parseInt(poljeStranica.val(), 10);

                //Provjera je li proslijeđen pomak
                if (stranica <= 0) { stranica = 1; }
                if (pomak) { stranica = stranica + pomak; }
                if (!pomak) { stranica = 1; }

                //Provjeri ograničenje stranica
                if (stranica > 0) {
                    $.get("/Ajax/BlogTablica.aspx", {
                        stranica: stranica
                    }, function (podaci) {
                        if ((pomak && podaci) || !pomak) {
                            tablica.html(podaci);
                            poljeStranica.val(stranica);
                        }
                    });
                }
            };

            //Navigacija slijedeæi artikli
            dugmicSlijedeca.click(function () {
                prikazi(+1);
            });

            //Navigacija prethodni artikli
            dugmicPrethodna.click(function () {
                prikazi(-1);
            });
        });
    };
    blog.shop();


    $(function () {
        $("#ddlNacinPlacanja").change(function () {
            try {
                if (this.options[this.selectedIndex].value == 'virman') {
                    document.getElementById("imgUplatnica").style.display = "block";
                } else {
                    document.getElementById("imgUplatnica").style.display = "none";
                }
                if (this.options[this.selectedIndex].value == 'mikrofin') {
                    document.getElementById("pMicrofin").style.display = "block";
                } else {
                    document.getElementById("pMicrofin").style.display = "none";
                } if (this.options[this.selectedIndex].value == 'kartice') {
                    document.getElementById("pMonri").style.display = "block";
                } else {
                    document.getElementById("pMonri").style.display = "none";
                }
            }
            catch (e) {
            }
        });
    });

})

//Brisanje poruke za restart lozinke, slanje kontakt forme i pogresnu lozinku
window.onload = function () {
    let porukaDiv = document.getElementById("reset-div");
    if (porukaDiv) {
        setTimeout(function () {
            porukaDiv.style.display = 'none';
        }, 3000);
    }
};

