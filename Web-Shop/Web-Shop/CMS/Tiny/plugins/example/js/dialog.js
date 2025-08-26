tinyMCEPopup.requireLangPack();

var ExampleDialog = {
    init: function () {
        var formular = $("#upload");
        formular.ajaxForm({
            dataType: "html",
            resetForm: true,
            timeout: 900000,
            success: function (podaci) {
                tinyMCEPopup.editor.execCommand('mceInsertContent', false, podaci);
                tinyMCEPopup.close();
            }
        });
    }
};

tinyMCEPopup.onInit.add(ExampleDialog.init, ExampleDialog);
