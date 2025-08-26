jQuery(document).ready(function ($) {

    //Dodavanje vezanog artikla
    $(".autocompleteVezaniArtikli").each(function () {
        var komponenta, poljeGrupa, lista, pojam, znakova, poljeGrupa, poljeArtikalId;
        komponenta = $(this);
        poljeGrupa = komponenta.find(".txtGrupa");
        poljeArtikalId = komponenta.find(".hidArtikalID");
        lista = komponenta.find(".listaGrupe");
        //Ne propagiraj klik događaj
        komponenta.click(function (e) {
            e.stopPropagation();
        });
        //Uzmi podatke
        poljeGrupa.on("keyup", function () {
            //alert("ok");
            delay(function () {
                pojam = $.trim(poljeGrupa.val());
                znakova = pojam.length;
                if (znakova > 2) {
                    $.get("/CMS/Ajax/AutocompleteListaArtikala.aspx", { pojam: pojam, artikalid: poljeArtikalId.val() }, function (podaci) {
                        if (podaci.length > 2) {
                            lista.html(podaci).show();
                        } else {
                            lista.html(podaci).hide();
                        };
                    });
                } else {
                    lista.hide();
                }
            }, 500);
        });
        poljeGrupa.on("click", function () {
            if (znakova > 2) {
                lista.show();
            }
        });
        //Odabir retka
        lista.on("click", ".redakT", function () {
            $.post("/CMS/Ajax/DopuniVezaniArtikal.aspx", {
                Id: $(this).data("id"),
                artikalid: poljeArtikalId.val()
            }, function (podaci) {
                location.reload();
                poljeGrupa.val("");
            });
            lista.hide();
        });
    }); 


    window.setTimeout(function () {
        //$("#alert-success").alert('close');
        //$("#alert-danger").alert('close');
        $(".sufee-alert").alert('close');
    }, 5000);

    if (typeof jQuery.fn.live == 'undefined' || !(jQuery.isFunction(jQuery.fn.live))) {
        jQuery.fn.extend({
            live: function (event, callback) {
                if (this.selector) {
                    jQuery(document).on(event, this.selector, callback);
                }
            }
        });
    }

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    var artikli = {};
    artikli.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".sviArtikli");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica, poljeNaziv;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblArtikli");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            poljeNaziv = komponenta.find(".txtNaziv");

            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                var naziv = poljeNaziv.val();
                $.get("/CMS/Ajax/ArtikliMrezaTablica.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        //poljeSlovoArtikla.val(slovo);
                    }
                });
            });
            dugmicPrethodna.click(function () {
                //alert("-1");
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                var naziv = poljeNaziv.val();
                //var slovo = poljeSlovoArtikla.val();
                if (stranica > 0) {
                    //komponenta.block();
                    $.get("/CMS/Ajax/ArtikliMrezaTablica.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                            //poljeSlovoArtikla.val(slovo);
                        }
                    });
                }
            });

            //poljeNaziv
            poljeNaziv.keyup(function () {
                delay(function () {
                    var stranica = parseInt(poljeStranica.val(), 10);
                    stranica = stranica;
                    var naziv = poljeNaziv.val();
                    $.get("/CMS/Ajax/ArtikliMrezaTablica.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                        //if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(1);
                        //poljeSlovoArtikla.val(slovo);
                        //}
                    });
                }, 500);
            });
        });
    };
    artikli.shop();

    var artikliNa = {};
    artikliNa.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".sviArtikliNa");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica, poljeNaziv;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblArtikli");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            poljeNaziv = komponenta.find(".txtNaziv");

            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                var naziv = poljeNaziv.val();
                $.get("/CMS/Ajax/ArtikliMrezaTablicaNa.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        //poljeSlovoArtikla.val(slovo);
                    }
                });
            });
            dugmicPrethodna.click(function () {
                //alert("-1");
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                var naziv = poljeNaziv.val();
                if (stranica > 0) {
                    //komponenta.block();
                    $.get("/CMS/Ajax/ArtikliMrezaTablicaNa.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                        }
                    });
                }
            });

            //poljeNaziv
            poljeNaziv.keyup(function () {
                delay(function () {
                    var stranica = parseInt(poljeStranica.val(), 10);
                    stranica = stranica;
                    var naziv = poljeNaziv.val();
                    $.get("/CMS/Ajax/ArtikliMrezaTablicaNa.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                        //if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(1);
                        //poljeSlovoArtikla.val(slovo);
                        //}
                    });
                }, 500);
            });

        });
    };
    artikliNa.shop();

    var artikliBezSlike = {};
    artikliBezSlike.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".sviArtikliBezSlike");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica, poljeNaziv;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblArtikliBezSlike");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            poljeKategorija = komponenta.find(".ddlKategorija");
            poljeNaziv = komponenta.find(".txtNaziv");
            //poljeSlovoArtikla = komponenta.find(".polje.hidSlovoArtikla");
            //dugmicSlovo = komponenta.find(".dugmic.slovo");

            //dugmicSlovo.click(function () {
            //    dugmic = $(this);
            //    poljeStranica.val(1);
            //    var stranica = parseInt(poljeStranica.val(), 10);
            //    poljeSlovoArtikla.val(dugmic.val());
            //    $(".dugmic.slovo").removeClass("active");
            //    var element = document.getElementById(dugmic.val());
            //    element.classList.toggle("active");
            //    var slovo = poljeSlovoArtikla.val();
            //    poljeStranica.val(1);
            //    $.get("/CMS/Ajax/ArtikliMrezaTablicaNa.aspx", { stranica: stranica, slovo: slovo }, function (podaci) {
            //        if (podaci.length > 0) {
            //            tablica.find(".tbody").html(podaci);
            //            poljeStranica.val(1);
            //            poljeSlovoArtikla.val(slovo);
            //        }
            //    });
            //});
            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                var kategorija = parseInt(poljeKategorija.val(), 10);
                var naziv = poljeNaziv.val();
                //alert(naziv);
                //komponenta.block();
                $.get("/CMS/Ajax/ArtikliMrezaTablicaBezSlike.aspx", { stranica: stranica, kategorija: kategorija, naziv: naziv }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        //poljeSlovoArtikla.val(slovo);
                    }
                });
            });
            dugmicPrethodna.click(function () {
                //alert("-1");
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                var kategorija = parseInt(poljeKategorija.val(), 10);
                var naziv = poljeNaziv.val();
                if (stranica > 0) {
                    //komponenta.block();
                    $.get("/CMS/Ajax/ArtikliMrezaTablicaBezSlike.aspx", { stranica: stranica, kategorija: kategorija, naziv: naziv }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                            //poljeSlovoArtikla.val(slovo);
                        }
                    });
                }
            });

            //poljeKategorija
            poljeKategorija.change(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica;
                var kategorija = parseInt(poljeKategorija.val(), 10);
                var naziv = poljeNaziv.val();
                $.get("/CMS/Ajax/ArtikliMrezaTablicaBezSlike.aspx", { stranica: stranica, kategorija: kategorija, naziv: naziv }, function (podaci) {
                    //if (podaci.length > 0) {
                    tablica.find(".tbody").html(podaci);
                    poljeStranica.val(1);
                    //poljeSlovoArtikla.val(slovo);
                    //}
                });
            });

            //poljeNaziv
            poljeNaziv.keyup(function () {
                delay(function () {
                    var stranica = parseInt(poljeStranica.val(), 10);
                    stranica = stranica;
                    var kategorija = parseInt(poljeKategorija.val(), 10);
                    var naziv = poljeNaziv.val();
                    $.get("/CMS/Ajax/ArtikliMrezaTablicaBezSlike.aspx", { stranica: stranica, kategorija: kategorija, naziv: naziv }, function (podaci) {
                        //if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(1);
                        //poljeSlovoArtikla.val(slovo);
                        //}
                    });
                }, 500);
            });

        });
    };
    artikliBezSlike.shop();

    var artikliIzdvojeni = {};
    artikliIzdvojeni.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".sviArtikliIzdvojeni");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica, poljeNaziv;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblArtikli");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            poljeNaziv = komponenta.find(".txtNaziv");

            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                var naziv = poljeNaziv.val();
                $.get("/CMS/Ajax/ArtikliMrezaTablicaIzdvojeni.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        //poljeSlovoArtikla.val(slovo);
                    }
                });
            });
            dugmicPrethodna.click(function () {
                //alert("-1");
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                var naziv = poljeNaziv.val();
                if (stranica > 0) {
                    //komponenta.block();
                    $.get("/CMS/Ajax/ArtikliMrezaTablicaIzdvojeni.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                        }
                    });
                }
            });

            //poljeNaziv
            poljeNaziv.keyup(function () {
                delay(function () {
                    var stranica = parseInt(poljeStranica.val(), 10);
                    stranica = stranica;
                    var naziv = poljeNaziv.val();
                    $.get("/CMS/Ajax/ArtikliMrezaTablicaIzdvojeni.aspx", { stranica: stranica, naziv: naziv }, function (podaci) {
                        //if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(1);
                        //poljeSlovoArtikla.val(slovo);
                        //}
                    });
                }, 500);
            });

        });
    };
    artikliIzdvojeni.shop();

    var clanci = {};
    clanci.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".sviClanci");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblClanci");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");

            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                $.get("/CMS/Ajax/ClanciMrezaTablica.aspx", { stranica: stranica }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        //poljeSlovoArtikla.val(slovo);
                    }
                });
            });
            dugmicPrethodna.click(function () {
                //alert("-1");
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                //var slovo = poljeSlovoArtikla.val();
                if (stranica > 0) {
                    //komponenta.block();
                    $.get("/CMS/Ajax/ClanciMrezaTablica.aspx", { stranica: stranica }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                            //poljeSlovoArtikla.val(slovo);
                        }
                    });
                }
            });
        });
    };
    clanci.shop();

    var StatusClanci = {};
    StatusClanci.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.statusClanci");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");

            chkBox.live("click", function () {
                //chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/ClanakStatus.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/ClanakStatus.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                        //element.style.display = "none";
                    });
                }
            });

        });
    };
    StatusClanci.mreza();

    var ArtikalIzmjena = {};
    ArtikalIzmjena.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".card.artikalDet");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, ddlKategorija, ddlPodKategorija, datadis, ddlKategorijaBulk, ddlPodKategorijaBulk, datadisBulk;
            //Pronađi elemente
            komponenta = $(this);
            ddlKategorija = komponenta.find(".ddlKategorija");
            datadis = ddlKategorija.data("dis");
            ddlPodKategorija = komponenta.find(".ddlPodKategorija");

            ddlKategorija.change(function () {
                //alert(ddlKategorija.val());
                if (parseInt(ddlKategorija.val(), 10) != 0) {
                    $.get("/CMS/Ajax/Potkategorije.aspx", { KategorijaID: ddlKategorija.val(), DisEna: datadis }, function (opcije) {
                        //alert(opcije);
                        ddlPodKategorija.html(opcije);
                    });
                } else {
                    ddlPodKategorija.html("<option value='0'>Najprije odaberite kategoriju</option>");
                }
            });

            ddlKategorijaBulk = komponenta.find(".ddlKategorijaBulk");
            datadisBulk = ddlKategorijaBulk.data("dis");
            ddlPodKategorijaBulk = komponenta.find(".ddlPodKategorijaBulk");

            ddlKategorijaBulk.change(function () {
                //alert(ddlKategorija.val());
                if (parseInt(ddlKategorijaBulk.val(), 10) != 0) {
                    $.get("/CMS/Ajax/PotkategorijeBulk.aspx", { KategorijaID: ddlKategorijaBulk.val(), DisEna: datadisBulk }, function (opcije) {
                        //alert(opcije);
                        ddlPodKategorijaBulk.html(opcije);
                    });
                } else {
                    ddlPodKategorijaBulk.html("<option value='0'>Najprije odaberite kategoriju</option>");
                }
            });

        });
    };
    ArtikalIzmjena.mreza();

    var StatusArtikli = {};
    StatusArtikli.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.statusArtikla");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");

            chkBox.live("click", function () {
                //chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/ArtikalStatus.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/ArtikalStatus.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                        //element.style.display = "none";
                    });
                }
            });

        });
    };
    StatusArtikli.mreza();

    var StatusKategorije = {};
    StatusKategorije.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.GrupeArtikala");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KategorijaStatus.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KategorijaStatus.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

        });
    };
    StatusKategorije.mreza();

    var StatusPodKategorije = {};
    StatusPodKategorije.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.PodKategorijeArtikala");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, btnPrioritetUp, btnPrioritetDown, tabela;
            //Pronađi elemente
            komponenta = $(this);
            body = komponenta.find(".tbody");
            chkBox = komponenta.find(".switch-input");
            btnPrioritetUp = komponenta.find(".btnPrioritetUp");
            btnPrioritetDown = komponenta.find(".btnPrioritetDown");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/PodKategorijaStatus.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/PodKategorijaStatus.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            btnPrioritetUp.live("click", function () {
                //btnPrioritetUp.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var nadgrupa = dugmic.data("nadgrupa");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetGrupe.aspx", { id: id, prioritet: prioritet, nadgrupa: nadgrupa, vrijednost: -1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    } else {
                        alert("Najveći prioritet je '1'");
                        element.classList.toggle('neaktivno');
                    }
                });
            });

            btnPrioritetDown.live("click", function () {
                //btnPrioritetDown.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var nadgrupa = dugmic.data("nadgrupa");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetGrupe.aspx", { id: id, prioritet: prioritet, nadgrupa: nadgrupa, vrijednost: 1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    }
                });
            });

        });
    };
    StatusPodKategorije.mreza();

    var artikliPodKategorije = {};
    artikliPodKategorije.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".sviArtikliPodKategorije");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica, podkategorijaid;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblArtikli");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            podkategorijaid = komponenta.find(".polje.PodKategorijaID");
            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                //var slovo = poljeSlovoArtikla.val();
                //alert(slovo);
                //komponenta.block();
                $.get("/CMS/Ajax/ArtikliMrezaTablicaPodKategorije.aspx", { stranica: stranica, podkategorijaid: podkategorijaid.val() }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        podkategorijaid.val(podkategorijaid.val());
                    }
                });
            });
            dugmicPrethodna.click(function () {
                //alert("-1");
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                //var slovo = poljeSlovoArtikla.val();
                if (stranica > 0) {
                    //komponenta.block();
                    $.get("/CMS/Ajax/ArtikliMrezaTablicaPodKategorije.aspx", { stranica: stranica, podkategorijaid: podkategorijaid.val() }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                            podkategorijaid.val(podkategorijaid.val());
                        }
                    });
                }
            });
        });
    };
    artikliPodKategorije.shop();

    var StatusSlideri = {};
    StatusSlideri.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.Slideri");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, btnPrioritetUp, btnPrioritetDown, tabela;
            //Pronađi elemente
            komponenta = $(this);
            body = komponenta.find(".tbody");
            chkBoxIgre = komponenta.find(".switch-input.actIgre");
            chkBoxBulk = komponenta.find(".switch-input.actBulk");
            btnPrioritetUp = komponenta.find(".btnPrioritetUp");
            btnPrioritetDown = komponenta.find(".btnPrioritetDown");

            chkBoxIgre.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                //element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/SlideriStatus.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/SlideriStatus.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            chkBoxBulk.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                //element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/SlideriStatusBulk.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/SlideriStatusBulk.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            btnPrioritetUp.live("click", function () {
                //btnPrioritetUp.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetSlidera.aspx", { id: id, prioritet: prioritet, vrijednost: -1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    } else {
                        alert("Najveći prioritet je '1'");
                        element.classList.toggle('neaktivno');
                    }
                });
            });

            btnPrioritetDown.live("click", function () {
                //btnPrioritetDown.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetSlidera.aspx", { id: id, prioritet: prioritet, vrijednost: 1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    }
                });
            });

        });
    };
    StatusSlideri.mreza();

    var StatusBrenda = {};
    StatusBrenda.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.Brendovi");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, btnPrioritetUp, btnPrioritetDown, tabela;
            //Pronađi elemente
            komponenta = $(this);
            body = komponenta.find(".tbody");
            chkBox = komponenta.find(".switch-input");
            btnPrioritetUp = komponenta.find(".btnPrioritetUp");
            btnPrioritetDown = komponenta.find(".btnPrioritetDown");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/BrendStatus.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/BrendStatus.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            btnPrioritetUp.live("click", function () {
                //btnPrioritetUp.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetBrenda.aspx", { id: id, prioritet: prioritet, vrijednost: -1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    } else {
                        alert("Najveći prioritet je '1'");
                        element.classList.toggle('neaktivno');
                    }
                });
            });

            btnPrioritetDown.live("click", function () {
                //btnPrioritetDown.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetBrenda.aspx", { id: id, prioritet: prioritet, vrijednost: 1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    }
                });
            });

        });
    };
    StatusBrenda.mreza();

    var StatusKorisnici = {};
    StatusKorisnici.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.Korisnici");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KorisnikStatus.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KorisnikStatus.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

        });
    };
    StatusKorisnici.mreza();

    var StatusNarudzbePoslano = {};
    StatusNarudzbePoslano.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.statusNarudzbePoslano");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");

            chkBox.live("click", function () {
                //chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                element.style.display = "none";
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/NarudzbaPoslana.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/NarudzbaPoslana.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                        //element.style.display = "none";
                    });
                }
            });

        });
    };
    StatusNarudzbePoslano.mreza();

    var StatusNarudzbeNaplaceno = {};
    StatusNarudzbeNaplaceno.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.statusNarudzbeNaplaceno");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox;
            //Pronađi elemente
            komponenta = $(this);
            chkBoxNaplaceno = komponenta.find(".switch-input.naplaceno");
            chkBoxNaplaceno.live("click", function () {
                //chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('zavrseno');
                //element.style.display = "none";
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/NarudzbaNaplacena.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/NarudzbaNaplacena.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                        //element.style.display = "none";
                    });
                }
            });

            chkBoxNPoslano = komponenta.find(".switch-input.poslano");
            chkBoxNPoslano.live("click", function () {
                //chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('zavrseno');
                element.style.display = "none";
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/NarudzbaPoslana.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/NarudzbaPoslana.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                        //element.style.display = "none";
                    });
                }
            });

        });
    };
    StatusNarudzbeNaplaceno.mreza();

    var narudzbeNove = {};
    narudzbeNove.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".noveNarudzbe");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica, poljeSlovoArtikla, dugmicSlovo;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblSveNarudzbe");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                $.get("/CMS/Ajax/NarudzbeNoveMrezaTablica.aspx", { stranica: stranica }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        //poljeSlovoArtikla.val(slovo);
                    }
                });
            });
            dugmicPrethodna.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                if (stranica > 0) {
                    $.get("/CMS/Ajax/NarudzbeNoveMrezaTablica.aspx", { stranica: stranica }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                        }
                    });
                }
            });
        });
    };
    narudzbeNove.shop();

    var narudzbeZavrsene = {};
    narudzbeZavrsene.shop = function () {
        //Pronađi sve komponente
        var komponente = $(".noveZavrsene");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, dugmicSlijedeca, dugmicPredhodna, poljeStranica, poljeSlovoArtikla, dugmicSlovo;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblSveNarudzbe");
            dugmicPrethodna = komponenta.find(".dugmic.prethodna");
            dugmicSlijedeca = komponenta.find(".dugmic.slijedeca");
            poljeStranica = komponenta.find(".polje.stranica");
            dugmicSlijedeca.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica + 1;
                $.get("/CMS/Ajax/NarudzbeZavrseneMrezaTablica.aspx", { stranica: stranica }, function (podaci) {
                    if (podaci.length > 0) {
                        tablica.find(".tbody").html(podaci);
                        poljeStranica.val(stranica);
                        //poljeSlovoArtikla.val(slovo);
                    }
                });
            });
            dugmicPrethodna.click(function () {
                var stranica = parseInt(poljeStranica.val(), 10);
                stranica = stranica - 1;
                if (stranica > 0) {
                    $.get("/CMS/Ajax/NarudzbeZavrseneMrezaTablica.aspx", { stranica: stranica }, function (podaci) {
                        if (podaci.length > 0) {
                            tablica.find(".tbody").html(podaci);
                            poljeStranica.val(stranica);
                        }
                    });
                }
            });
        });
    };
    narudzbeZavrsene.shop();

    var StatusComTradeKategorije = {};
    StatusComTradeKategorije.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.ComTradeKategorije");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, ddlUniverzalGrupeComTrade;
            //Pronađi elemente
            komponenta = $(this);
            ddlUniverzalGrupeComTrade = komponenta.find(".ddlUniverzalGrupeComTrade");
            chkBoxIgre = komponenta.find(".switch-input.btnIgreAct");
            ddlBulkGrupeComTrade = komponenta.find(".ddlBulkGrupeComTrade");
            chkBoxBulk = komponenta.find(".switch-input.btnBulkAct");

            chkBoxIgre.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var grpuni = dugmic.data("grpuni");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KategorijaStatusComTrade.aspx", { id: id, status: 1, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KategorijaStatusComTrade.aspx", { id: id, status: 0, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            ddlUniverzalGrupeComTrade.live("change", function () {
                //var dugmic = $(this);
                var unigrupa = $(this).val();
                var comgrupa = $(this).find(':selected').attr('data-id');
                $.get("/CMS/Ajax/PridruziGrupe.aspx", { unigrupa: unigrupa, comgrupa: comgrupa }, function (opcije) {
                    //alert(opcije);
                });
            });

            chkBoxBulk.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var grpuni = dugmic.data("grpuni");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KategorijaStatusComTradeBulk.aspx", { id: id, status: 1, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KategorijaStatusComTradeBulk.aspx", { id: id, status: 0, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            ddlBulkGrupeComTrade.live("change", function () {
                //var dugmic = $(this);
                var unigrupa = $(this).val();
                var comgrupa = $(this).find(':selected').attr('data-id');
                $.get("/CMS/Ajax/PridruziGrupeBulk.aspx", { unigrupa: unigrupa, comgrupa: comgrupa }, function (opcije) {
                    //alert(opcije);
                });
            });

        });
    };
    StatusComTradeKategorije.mreza();

    var StatusUniExpertKategorije = {};
    StatusUniExpertKategorije.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.UniExpertKategorije");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, ddlUniverzalGrupeUniExpert;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");
            ddlUniverzalGrupeUniExpert = komponenta.find(".ddlUniverzalGrupeUniExpert");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var grpuni = dugmic.data("grpuni");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KategorijaStatusUniExpert.aspx", { id: id, status: 1, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KategorijaStatusUniExpert.aspx", { id: id, status: 0, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            ddlUniverzalGrupeUniExpert.live("change", function () {
                //var dugmic = $(this);
                var unigrupa = $(this).val();
                var comgrupa = $(this).find(':selected').attr('data-id');
                $.get("/CMS/Ajax/PridruziGrupeUniExpert.aspx", { unigrupa: unigrupa, comgrupa: comgrupa }, function (opcije) {
                    //alert(opcije);
                });
            });

        });
    };
    StatusUniExpertKategorije.mreza();

    var StatusStarTechKategorije = {};
    StatusStarTechKategorije.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.StarTechKategorije");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, ddlUniverzalGrupeUniExpert;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");
            ddlUniverzalGrupeUniExpert = komponenta.find(".ddlUniverzalGrupeStarTech");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var grpuni = dugmic.data("grpuni");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KategorijaStatusStarTech.aspx", { id: id, status: 1, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KategorijaStatusStarTech.aspx", { id: id, status: 0, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            ddlUniverzalGrupeUniExpert.live("change", function () {
                //var dugmic = $(this);
                var unigrupa = $(this).val();
                var comgrupa = $(this).find(':selected').attr('data-id');
                $.get("/CMS/Ajax/PridruziGrupeStarTech.aspx", { unigrupa: unigrupa, comgrupa: comgrupa }, function (opcije) {
                    //alert(opcije);
                });
            });

        });
    };
    StatusStarTechKategorije.mreza();

    var StatusDigitalisKategorije = {};
    StatusDigitalisKategorije.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.DigitalisKategorije");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, ddlUniverzalGrupeUniExpert;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");
            ddlUniverzalGrupeUniExpert = komponenta.find(".ddlUniverzalGrupeDigitalis");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var grpuni = dugmic.data("grpuni");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KategorijaStatusDigitalis.aspx", { id: id, status: 1, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KategorijaStatusDigitalis.aspx", { id: id, status: 0, grpuni: grpuni }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            ddlUniverzalGrupeUniExpert.live("change", function () {
                //var dugmic = $(this);
                var unigrupa = $(this).val();
                var comgrupa = $(this).find(':selected').attr('data-id');
                $.get("/CMS/Ajax/PridruziGrupeDigitalis.aspx", { unigrupa: unigrupa, comgrupa: comgrupa }, function (opcije) {
                    //alert(opcije);
                });
            });

        });
    };
    StatusDigitalisKategorije.mreza();


    var fnPopustNaKolicinu = {};
    fnPopustNaKolicinu.mreza = function () {
        //Pronađi sve komponente
        var komponente = $(".card.artikalPopusti");
        //Primjeni na sve komponente
        komponente.each(function () {

            //Varijable
            var komponenta, tablica, btnSpremiPopust, poljeKolicina, poljePopust;
            //Pronađi elemente
            komponenta = $(this);
            tablica = komponenta.find(".tblPopusti");
            btnSpremiPopust = komponenta.find(".btnSpremiPopust");
            hidArtikalId = komponenta.find(".hidArtikalId");
            poljeKolicina = komponenta.find("#txtKolicina");
            poljePopust = komponenta.find("#txtPopust");

            btnSpremiPopust.live("click", function () {
                //if (poljeVelicina.val() !== '') {
                    //btnSpremiVelicinu.click(function () {
                    var kolicina = poljeKolicina.val();
                    var popust = poljePopust.val();
                    var artikalid = hidArtikalId.val();
                    $.post("/CMS/Ajax/DodajPopustArtiklu.aspx", { kolicina: kolicina, popust: popust, artikalid: artikalid },
                        function (podaci) {
                            if (podaci.length > 0) {
                                tablica.html(podaci);
                            }
                        }
                    );
                /*}*/
            });

            var btnUkloniPopust = tablica.find(".btnUkloniPopust");
            btnUkloniPopust.live("click", function () {
                dugmic = $(this);
                var popustid = dugmic.data("id");
                var artikalid = dugmic.data("artikal");
                if (confirm('Želite li izbrisati zapis')) {
                    $.post("/CMS/Ajax/UkloniPopustArtiklu.aspx", { popustid: popustid, artikalid: artikalid },
                        function (podaci) {
                            if (podaci.length > 0) {
                                tablica.html(podaci);
                            }
                        }
                    );
                }
            });

        });
    };
    fnPopustNaKolicinu.mreza();

    var StatusKategorijeBulk = {};
    StatusKategorijeBulk.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.GrupeArtikalaBulk");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox;
            //Pronađi elemente
            komponenta = $(this);
            chkBox = komponenta.find(".switch-input");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/KategorijaStatusBulk.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/KategorijaStatusBulk.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

        });
    };
    StatusKategorijeBulk.mreza();

    var StatusPodKategorijeBulk = {};
    StatusPodKategorijeBulk.mreza = function () {
        //Pronađi sve komponente
        //var komponente = $(".mreza");
        var komponente = $(".row.PodKategorijeArtikalaBulk");
        //Primjeni na sve komponente
        komponente.each(function () {
            //Varijable
            var komponenta, chkBox, btnPrioritetUp, btnPrioritetDown, tabela;
            //Pronađi elemente
            komponenta = $(this);
            body = komponenta.find(".tbody");
            chkBox = komponenta.find(".switch-input");
            btnPrioritetUp = komponenta.find(".btnPrioritetUp");
            btnPrioritetDown = komponenta.find(".btnPrioritetDown");

            chkBox.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                if ($(this).is(':checked')) {
                    $.get("/CMS/Ajax/PodKategorijaStatusBulk.aspx", { id: id, status: 1 }, function (opcije) {
                        //alert(opcije);
                    });
                } else {
                    $.get("/CMS/Ajax/PodKategorijaStatusBulk.aspx", { id: id, status: 0 }, function (opcije) {
                        //alert(opcije);
                    });
                }
            });

            btnPrioritetUp.live("click", function () {
                //btnPrioritetUp.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var nadgrupa = dugmic.data("nadgrupa");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetGrupeBulk.aspx", { id: id, prioritet: prioritet, nadgrupa: nadgrupa, vrijednost: -1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    } else {
                        alert("Najveći prioritet je '1'");
                        element.classList.toggle('neaktivno');
                    }
                });
            });

            btnPrioritetDown.live("click", function () {
                //btnPrioritetDown.click(function () {
                var dugmic = $(this);
                var id = dugmic.data("id");
                var prioritet = dugmic.data("prioritet");
                var nadgrupa = dugmic.data("nadgrupa");
                var element = document.getElementById(id);
                element.classList.toggle('neaktivno');
                $.get("/CMS/Ajax/PromjeniPrioritetGrupeBulk.aspx", { id: id, prioritet: prioritet, nadgrupa: nadgrupa, vrijednost: 1 }, function (podaci) {
                    if (podaci.length > 0) {
                        body.html(podaci);
                    }
                });
            });

        });
    };
    StatusPodKategorijeBulk.mreza();

    $(".tiny").tinymce({
        script_url: "/cms/tiny/tiny_mce.js?v=01",
        language: "hr",
        theme: "advanced",
        plugins: "paste,inlinepopups,media,example,table,advlist",
        dialog_type: "modal",
        width: "100%",
        height: "330px",
        convert_urls: true,
        theme_advanced_buttons1: "",
        theme_advanced_buttons1: "bold,italic,underline, |,justifyleft,justifycenter,justifyright,justifyfull,|,forecolor,backcolor,|,bullist,numlist,outdent,indent,|,table,link",
                        //theme_advanced_buttons2: "image,example,|,link,unlink,|,code",
        setup: function (editor) {
            editor.onInit.add(function (editor) {
                editor.pasteAsPlainText = true;
            });
        }
    });

    $(".tinyMax").tinymce({
        script_url: "/cms/tiny/tiny_mce.js?v=01",
        language: "hr",
        theme: "advanced",
        plugins: "paste,inlinepopups,media,example,table,advlist",
        dialog_type: "modal",
        width: "900px",
        height: "530px",
        convert_urls: true,
        theme_advanced_buttons1: "",
        theme_advanced_buttons1: "bold,italic,underline, |,justifyleft,justifycenter,justifyright,justifyfull,|,forecolor,backcolor,|,bullist,numlist,outdent,indent,|,table,link,unlink,|,code",
        //theme_advanced_buttons2: "image,example,|,link,unlink,|,code",
        setup: function (editor) {
            editor.onInit.add(function (editor) {
                editor.pasteAsPlainText = true;
            });
        }
    });

  
    //$('#imagefile').on('change', function () {

    //   // $.blockUI.defaults.message = "<br/>PODIŽEM DATOTEKU ...<br/><br/>"
    //   // $.blockUI.defaults.overlayCSS = { backgroundColor: '#fff', opacity: .3 }
        
    //    var dugmic;
    //    dugmic = $(this);
    //    var fd = new FormData();

    //    fd.append("fileInput", $(this)[0].files[0]);
    //    var slika;
    //    slika = $('#fileInput').val();
    //    fd.append("id", dugmic.data("id"));
    //    var redak = dugmic.parents(".tr." + id).eq(0);
    //    var id = redak.data("id");
    //    //alert(redak.data("id"));
    //    $.ajax({
    //        url: '/CMS/Ajax/UploadMobilnogSlidera.aspx',
    //        type: 'POST',
    //        cache: false,
    //        data: fd,
    //        processData: false,
    //        contentType: false,

    //        beforeSend: function () {
    //            //tabela.block();
    //        },
    //        success: function (podaci) {
    //            //tabela.unblock();
    //            //window.location = "/CMS/Slideri.aspx"
    //        }
    //    });
    //});

      //Dodavanje mobilnog slidera
    $(document).on('click', '.imagefileDodaj', function () {
        var id = $(this).data("id");
        $('#file_' + id).trigger('click');
    });

    // Kada korisnik izabere sliku
    $(document).on('change', '.imagefile', function () {
        var input = $(this);
        var id = input.data("id");
        var fd = new FormData();

        fd.append("fileInput", input[0].files[0]);
        fd.append("id", id);

        $.ajax({
            url: '/CMS/Ajax/UploadMobilnogSlidera.aspx',
            type: 'POST',
            cache: false,
            data: fd,
            processData: false,
            contentType: false,
            success: function (podaci) {
                location.reload(); // ili updateaj samo jedan red bez reloada
            }
        });
    });



    //Brisanje mobilnog slidera
    $('.imagefileUkloni').on('click', function () {

        var strconfirm = confirm("Želite li ukloniti datoteku?");
        if (strconfirm == true) {
          //  $.blockUI.defaults.message = "<br/>UKLANJAM DATOTEKU ...<br/><br/>"
          //  $.blockUI.defaults.overlayCSS = { backgroundColor: '#fff', opacity: .3 }

            var dugmic, divPrivitci, id, spis;
            dugmic = $(this);
            //tabela = dugmic.parents(".grdClanci tbody tr");
            id = dugmic.data("id");
            file = dugmic.data("file");
            //alert(file);
            //tabela.block();
            $.get("/CMS/Ajax/DeleteMobilnogSlider.aspx", {
                id: id,
                file: file
            }, function (podaci) {
                //tabela.unblock();
                window.location = "/CMS/Slideri.aspx"
            });
        }
    });

});