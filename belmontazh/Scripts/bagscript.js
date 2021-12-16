function editCartDveri(obj) {
    var idDveri = $(obj).attr("data-id");
    var countDveri = parseFloat($(obj).val().replace('.', ','));
    if (window.FormData !== undefined) {
        var fileData = new FormData();
        fileData.append("idDveri", idDveri);
        fileData.append("count", countDveri);
        $.ajax({
            url: "/cart/editCartDveri",
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                calculateCart();
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}
function editCartKomplekt(obj) {
    var idKomplekt = $(obj).attr("data-id");
    var countDveri = parseFloat($(obj).val().replace('.', ','));
    if (window.FormData !== undefined) {
        var fileData = new FormData();
        fileData.append("idKomplekt", idKomplekt);
        fileData.append("count", countDveri);
        $.ajax({
            url: "/cart/editCartKomplekt",
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                calculateCart();
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}
function calculateCart() {
    var cost = 0;
    var fullCost = 0;    
    $(".table").find("tr").each(function () {        
        if ($(this).find("input[type=text]").length > 0) {
            cost = (parseFloat($(this).find("b").text().replace(',', '.')) * parseFloat($(this).find("input[type=text]").val().replace(',', '.')));
            fullCost += cost;
            $(this).find("strong").text(cost.toFixed(2));
        }
    });
    $("#allcost").html("Итого: <b>" + fullCost.toFixed(2) + "</b> BYN");
}
function addCart() {
    $("#loading").css("display", "block");
    var idDveri = $("#id").val();
    var countDveri = $("#count").val();
    var komplekt = new Array();
    $(".komplektForm").find("tr").each(function () {
        if ($(this).find("input[type=checkbox]").is(":checked")) {
            komplekt.push({ "id": $(this).find("input[type=hidden]").val(), "count": $(this).find("input[type=text]").val().replace('.', ',') });
        }
    });
    var cartForm = new Object();
    cartForm.id = idDveri;
    cartForm.count = countDveri.replace('.', ',');
    cartForm.komplekt = komplekt;
    if (window.FormData !== undefined) {        
        var fileData = new FormData();
        $.ajax({
            url: "/cart/addcart",
            type: "POST",
            contentType: "application/json; charset=utf-8", // Not to set any content header  
            processData: false, // Not to process data  
            data: JSON.stringify(cartForm),
            success: function (result) {
                $('#cartUser').load('/cart/viewCart');
                $("#loading").css("display", "none");
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}

function addkomplektCheckBox(obj)
{
    if($(obj).is(":checked"))
    {
        $(obj).parent().find("input[type=hidden]").val($(obj).data("val"));
    }
    else
    {
        $(obj).parent().find("input[type=hidden]").val("");
    }
}
function changeCountDveri() {
    $(".komplektForm").find("input[type=text]").each(function () {
        var valueInput = $(this).attr("data-defaultValue");
        $(this).val(parseFloat(valueInput.replace(',', '.')) * parseFloat($("#count").val().replace(',', '.')));
    });
    calculateDveri();
}

function checkKomplektDveri(obj)
{
    var parent = $(obj).parents("tr");
    var input = parent.find("[data-cost]");
    if ($(obj).is(":checked")) {       
        parent.find("strong").text((parseFloat(input.val().replace(',', '.')) * parseFloat(input.attr("data-cost").replace(',', '.'))));
        calculateDveri();
    }
    else
    {
        input.css("display", "none");
        parent.find("span").css("display", "none");
        calculateDveri();
    }
    
}
function calculateDveri() {
    var cost = 0;
    var fullCost = 0;
   
    $(".komplektForm").find("tr").each(function () {
        if ($(this).find("input[type=checkbox]").is(":checked")) {
            cost = (parseFloat($(this).find("input[type=text]").attr("data-cost").replace(',', '.')) * parseFloat($(this).find("input[type=text]").val().replace(',', '.')));
            fullCost += cost;
            $(this).find("strong").text(cost.toFixed(2));
            $(this).find("input[type=text]").css("display", "block");
            $(this).find("span").css("display", "block");
        }
    });    
    $("#costKomplekt").html("Итого: <b>" + parseFloat(fullCost + parseFloat($("#cost").text().replace(',', '.')) * parseFloat($("#count").val().replace(',', '.'))).toFixed(2) + "</b> руб");
}

function removeImageDveri(obj)
{
    $(obj).parent().find('input[type=hidden]').val('');
    $(obj).parent().css("display", "none");
}

function processData(data)
{
    var target = $("#resultsCost");
    target.empty();
    var js = JSON.parse(data);
    if (js.cost!=null)
    {
        target.append("<h4>Стоимость окна: <b>" + js.cost + " BYN</b></h4>");
        target.append("<p>Отлив: <b>" + js.otliv + " BYN</b></p>");
        target.append("<p>Подоконник: <b>" + js.podokonik + " BYN</b></p>");
        target.append("<p>Отделка откосов: <b>" + js.otkos + " BYN</b></p>");
        target.append("<p>Москитная сетка: <b>" + js.moskit + " BYN</b></p>");
        target.append("<p>Монтаж: <b>" + js.montazh + " BYN</b></p>");
        target.append('<div class="line"></div>');
        target.append("<h4>Итого: <b>" + js.itog + " BYN</b></h4>");
    }
    else
    {
        for(var i = 0;i<js.length;i++)
        {
            target.append("<p>" + js[i] + "</p>");
        }
    }
}

function fullImgCalculate(obj)
{
    $("#full_img").attr("src", $(obj).find("img").attr("src"));
}
function calculatePrice()
{
    var cost = 0;
    $(".table").find("tr").each(function () {
        if ($(this).find("input[type=text]").length>0) {
            cost = ($(this).find("span").text().replace(',', '.') * $(this).find("input[type=text]").val().replace(',', '.'));
            $(this).find("strong").text(cost.toFixed(2));
        }
    });
    cost = 0;
    $(".table").find("strong").each(function () {
        cost += parseFloat($(this).text());
    });
    $("#cost").text(cost);
}
function replaseForm()
{
    $(".form-horizontal").find("input[type=text]").each(function () {
        var val = $(this).val();
        $(this).val(val.replace('.', ','));
    });
}
//function addProperty()
//{
//    $(window).load("/admin/dveri/valuePropertyResult", function (response, status, xhr) {
//        var id = -1;
//        var minId = 1;
//        $("#propertyValue").find('[id*=propertyDveriModelid]').each(function () {            
//            minId = $(this).attr("id").match(/[0-9]/g);            
//            if (parseInt(minId) > id) id = minId;
//        });

//        id++;

//        $("#propertyValue").append(response);        
//        $("#propertyValue").find("#propertyDveriModelid").attr('name', 'valueProperty[' + id + '].propertyDveriModelid');
//        $("#propertyValue").find("#propertyDveriModelid").attr('id', 'valueProperty[' + id + '].propertyDveriModelid');
//        $("#propertyValue").find("#valueProp").attr('name', 'valueProperty[' + id + '].valueProp');
//        $("#propertyValue").find("#valueProp").attr('id', 'valueProperty[' + id + '].valueProp');
//    });
//}
function addPropertyKrovlia() {
    $(window).load("/admin/Krovlia/valuePropertyResult", function (response, status, xhr) {
        var id = -1;
        var minId = 1;
        $("#propertyValue").find('[id*=krovliaTypeModelid]').each(function () {
            minId = $(this).attr("id").match(/[0-9]/g);
            if (parseInt(minId) > id) id = minId;
        });

        id++;

        $("#propertyValue").append(response);
        $("#propertyValue").find("#krovliaTypeModelid").attr('name', 'krovliaTypeValueModel[' + id + '].krovliaTypeModelid');
        $("#propertyValue").find("#krovliaTypeModelid").attr('id', 'krovliaTypeValueModel[' + id + '].krovliaTypeModelid');
        $("#propertyValue").find("#cost").attr('name', 'krovliaTypeValueModel[' + id + '].cost');
        $("#propertyValue").find("#cost").attr('id', 'krovliaTypeValueModel[' + id + '].cost');
    });
}
function editKomplekt(id, name, cost, defaultValue, defaultSet) {
    $("form").append("<input type='hidden' id='id' name='id' value='" + id + "'/>");
    $("#name").val(name);
    $("#cost").val(cost);
    $("#defaultValue").val(defaultValue);
    $("#defaultSet").attr("checked", defaultSet);
    $("#name").focus();
}
function editProperty(id, name) {
    $("form").append("<input type='hidden' id='id' name='id' value='" + id + "'/>");
    $("#name").val(name);
    $("#name").focus();
}

function editOkno(id, name, cost, units, url, count, col) {
    $("form").append("<input type='hidden' id='id' name='id' value='" + id + "'/>");
    $("#name").val(name);
    $("#cost").val(cost);
    if (units != '')
        $("#unitsModelid").val(units);
    if (url != '')
        $("#urlImage").val(url);
    if (count != '')
        $("#count").val(count);
    if (col != '')
        $("#col").val(col);
    $("#name").focus();
}


function editProizvoditel(id, name, description, country) {
    $("form").append("<input type='hidden' id='id' name='id' value='" + id + "'/>");
    $("#name").val(name);
    $("#description").val(description);
    if (country != '')
        $("#country").val(country);
    $("#name").focus();
}

function openFoto(obj)
{
    $("#myModal").modal();
    $("#myModal").css('text-align', 'center');
    $(".modal-dialog").width("auto");
    $(".modal-dialog").css('display', 'inline-block');
    $(".modal-body").html("<img style='max-width:600px;max-height:550px;' src='" + $(obj).attr("data-image") + "'/>");
    $("[data-slide=prev]").click(function () {
        nextImg(-1);
    });
    $("[data-slide=next]").click(function () {
        nextImg(+1);
    });
}
function nextImg(increment)
{
    var arrayImage = $("img[data-image]");    
    var thisImage = $(".modal-body").find("img")[0];
    var thisImageUrl = thisImage.src;
    var count = 5;
    for (var i = 0; i < arrayImage.length; i++)
    {        
        if (thisImageUrl.indexOf($(arrayImage[i]).attr("data-image"))!=-1)
        {
            count = i + increment;
            if (count < 0) count = arrayImage.length-1;
            if (count >= arrayImage.length) count = 0;
            break;
        }
    }
    thisImage.src = $(arrayImage[count]).attr("data-image");
}

function enableEditor()
{
    if ($("#editorPanel").css('display')=="none") {
        $("[data-target='#editor']").html(baggeteditor.geteditortext().innerHTML);
        $('#editor').wysiwyg();
        $("#editor").html($("#content").val());
        $("#content").hide("fast");
        $("#editorPanel").show("fast");
        $("[data-target='#editorPanel']").html("Закрыть редактор");
    }
    else
    {
        $("#content").val($("#editor").html());
        $("#content").show("fast");
        $("#editorPanel").hide("fast");
        $("[data-target='#editorPanel']").html("Загрузить редактор");
    }
}
function disableEditor() {
    $("#content").show("fast");
    $("#editorPanel").hide("fast");
}

var baggeteditor = {    
    geteditortext: function () {
        
        var editorTolbar = document.createElement("div");
        var divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");
        
        $(divMenu).append($('<a class="btn btn-default dropdown-toggle" data-toggle="dropdown" title="Font size"><i class="fa fa-font"></i><b class="caret"></b></a>'));
                
        var fonts = ['Serif', 'Sans', 'Arial', 'Arial Black', 'Courier',
                   'Courier New', 'Comic Sans MS', 'Helvetica', 'Impact', 'Lucida Grande', 'Lucida Sans', 'Tahoma', 'Times',
                   'Times New Roman', 'Verdana'];


        var select = document.createElement("ul");
        select.setAttribute("class", "dropdown-menu");
        
        for (var i = 0; i < fonts.length; i++) {
            $(select).append($('<li><a data-edit="fontName ' + fonts[i] + '" style="font-family:\'' + fonts[i] + '\'">' + fonts[i] + '</a></li>'));
        }

        divMenu.appendChild(select);
        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");

        $(divMenu).append($('<a class="btn btn-default dropdown-toggle" data-toggle="dropdown" title="Font Size"><i class="fa fa-text-height"></i><b class="caret"></b></a>'));


        var select = document.createElement("ul");
        select.setAttribute("class", "dropdown-menu");
        for (var i = 8; i < 30;) {
            $(select).append($('<li><a data-edit="fontSize ' + i + '" style="font-size:' + i + 'px;">Font Size ' + i + 'px</a></li>'));
            i = i+2;
        }

        divMenu.appendChild(select);
        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");

        
        $(divMenu).append($('<a class="btn btn-default dropdown-toggle" data-toggle="dropdown" title="Font Color"><i class="fa fa-font"></i><b class="caret"></b></a>'));

        var select = document.createElement("ul");
        select.setAttribute("class", "dropdown-menu");

        $(select).append($('<li><a data-edit="foreColor  #000000" style="color:#000000;">Black</a></li>'));
        $(select).append($('<li><a data-edit="foreColor  #00FFFF" style="color:#00FFFF;">Blue</a></li>'));
        $(select).append($('<li><a data-edit="foreColor  #00FF00" style="color:#00FF00;">Green</a></li>'));
        $(select).append($('<li><a data-edit="foreColor  #FF7F00" style="color:#FF7F00;">Orange</a></li>'));
        $(select).append($('<li><a data-edit="foreColor  #FF0000" style="color:#FF0000;">Red</a></li>'));
        $(select).append($('<li><a data-edit="foreColor  #FFFF00" style="color:#FFFF00;">Yellow</a></li>'));


        divMenu.appendChild(select);
        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");


        $(divMenu).append($('<a class="btn btn-default" data-edit="bold" title="Bold (Ctrl/Cmd+B)"><i class="fa fa-bold"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="italic" title="italic (Ctrl/Cmd+I)"><i class="fa fa-italic"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="strikethrough" title="strikethrough (Ctrl/Cmd+B)"><i class="fa fa-strikethrough"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="underline" title="underline (Ctrl/Cmd+U)"><i class="fa fa-underline"></i></a>'));


        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");

        $(divMenu).append($('<a class="btn btn-default" data-edit="insertunorderedlist" title="Bullet list"><i class="fa fa-list-ul"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="insertorderedlist" title="Number list"><i class="fa fa-list-ol"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="outdent" title="Reduce indent (Shift+Tab)"><i class="fa fa-outdent"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="indent" title="Indent (Tab)"><i class="fa fa-indent"></i></a>'));

        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");


        $(divMenu).append($('<a class="btn btn-default" data-edit="justifyleft" title="Align Left (Ctrl/Cmd+L)"><i class="fa fa-align-left"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="justifycenter" title="Center (Ctrl/Cmd+E)"><i class="fa fa-align-center"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="justifyright" title="Align Right (Ctrl/Cmd+R)"><i class="fa fa-align-right"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="justifyfull" title="Justify (Ctrl/Cmd+J)"><i class="fa fa-align-justify"></i></a>'));

        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");

        $(divMenu).append($('<a class="btn btn-default dropdown-toggle" data-toggle="dropdown" title="Hyperlink"><i class="fa fa-link"></i></a>'));
        $(divMenu).append($('<div class="dropdown-menu input-append"><input placeholder="URL" type="text" data-edit="createLink" /><button class="btn btn-default" type="button">Add</button></div>'));
        
        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");

        $(divMenu).append($('<a class="btn btn-default dropdown-toggle" data-edit="unlink" title="Remove Hyperlink"><i class="fa fa-unlink"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="insertImage" title="Insert picture (or just drag & drop)"><i class="fa fa-picture-o"></i></span>'));

        editorTolbar.appendChild(divMenu);
        divMenu = document.createElement("div");
        divMenu.setAttribute("class", "btn-group");


        $(divMenu).append($('<a class="btn btn-default" data-edit="undo" title="Undo (Ctrl/Cmd+Z)"><i class="fa fa-undo"></i></a>'));
        $(divMenu).append($('<a class="btn btn-default" data-edit="redo" title="Redo (Ctrl/Cmd+Y)"><i class="fa fa-repeat"></i></a>'));

        editorTolbar.appendChild(divMenu);


        return editorTolbar;

    }

   
};



(function (window, $) {
    "use strict";
    /*
     *  Represenets an editor
     *  @constructor
     *  @param {DOMNode} element - The TEXTAREA element to add the Wysiwyg to.
     *  @param {object} userOptions - The default options selected by the user.
     */
    function Wysiwyg(element, userOptions) {
        // This calls the $ function, with the element as a parameter and
        // returns the jQuery object wrapper for element. It also assigns the
        // jQuery object wrapper to the property $editor on `this`.
        this.selectedRange = null;
        this.editor = $(element);
        var editor = $(element);
        var defaults = {
            toolbarSelector: "[data-role=editor-toolbar]",
            commandRole: "edit",
            activeToolbarClass: "btn-info",
            selectionMarker: "edit-focus-marker",
            selectionColor: "darkgrey",
            dragAndDropImages: true,
            keypressTimeout: 200,
            fileUploadError: function (reason, detail) { console.log("File upload error", reason, detail); }
        };

        var options = $.extend(true, {}, defaults, userOptions);
        var toolbarBtnSelector = "a[data-" + options.commandRole + "],button[data-" + options.commandRole + "],input[type=button][data-" + options.commandRole + "]";
        this.bindHotkeys(editor, options, toolbarBtnSelector);
                
        this.bindToolbar(editor, $(options.toolbarSelector), options, toolbarBtnSelector);

        editor.attr("contenteditable", true)
            .on("mouseup keyup mouseout", function () {
                this.saveSelection();
                this.updateToolbar(editor, toolbarBtnSelector, options);
            }.bind(this));

        $(window).bind("touchend", function (e) {
            var isInside = (editor.is(e.target) || editor.has(e.target).length > 0),
            currentRange = this.getCurrentRange(),
            clear = currentRange && (currentRange.startContainer === currentRange.endContainer && currentRange.startOffset === currentRange.endOffset);

            if (!clear || isInside) {
                this.saveSelection();
                this.updateToolbar(editor, toolbarBtnSelector, options);
            }
        });
    }
    
    Wysiwyg.prototype.updateToolbar = function (editor, toolbarBtnSelector, options) {
        if (options.activeToolbarClass) {
            $(options.toolbarSelector).find(toolbarBtnSelector).each(function () {
                var self = $(this);
                var commandArr = self.data(options.commandRole).split(" ");
                var command = commandArr[0];

                // If the command has an argument and its value matches this button. == used for string/number comparison
                if (commandArr.length > 1 && document.queryCommandEnabled(command) && document.queryCommandValue(command) === commandArr[1]) {
                    self.addClass(options.activeToolbarClass);
                }

                    // Else if the command has no arguments and it is active
                else if (commandArr.length === 1 && document.queryCommandEnabled(command) && document.queryCommandState(command)) {
                    self.addClass(options.activeToolbarClass);
                }

                    // Else the command is not active
                else {
                    self.removeClass(options.activeToolbarClass);
                }
            });
        }
    };

    Wysiwyg.prototype.execCommand = function (commandWithArgs, valueArg, editor, options, toolbarBtnSelector) {
        var commandArr = commandWithArgs.split(" "),
            command = commandArr.shift(),
            args = commandArr.join(" ") + (valueArg || "");

        var parts = commandWithArgs.split("-");

        if (parts.length === 1) {
            document.execCommand(command, false, args);
        } else if (parts[0] === "format" && parts.length === 2) {
            document.execCommand("formatBlock", false, parts[1]);
        }

        (editor).trigger("change");
        this.updateToolbar(editor, toolbarBtnSelector, options);
    };

    Wysiwyg.prototype.bindHotkeys = function (editor, options, toolbarBtnSelector) {
        var self = this;
        $.each(options.hotKeys, function (hotkey, command) {
            if (!command) return;

            $(editor).keydown(hotkey, function (e) {
                if (editor.attr("contenteditable") && $(editor).is(":visible")) {
                    e.preventDefault();
                    e.stopPropagation();
                    self.execCommand(command, null, editor, options, toolbarBtnSelector);
                }
            }).keyup(hotkey, function (e) {
                if (editor.attr("contenteditable") && $(editor).is(":visible")) {
                    e.preventDefault();
                    e.stopPropagation();
                }
            });
        });

        editor.keyup(function () { editor.trigger("change"); });
    };

    Wysiwyg.prototype.getCurrentRange = function () {
        var sel, range;
        if (window.getSelection) {
            sel = window.getSelection();
            if (sel.getRangeAt && sel.rangeCount) {
                range = sel.getRangeAt(0);
            }
        } else if (document.selection) {
            range = document.selection.createRange();
        }

        return range;
    };

    Wysiwyg.prototype.saveSelection = function () {
        this.selectedRange = this.getCurrentRange();
    };

    Wysiwyg.prototype.restoreSelection = function () {
        var selection;
        if (window.getSelection || document.createRange) {
            selection = window.getSelection();
            if (this.selectedRange) {
                try {
                    selection.removeAllRanges();
                }
                catch (ex) {
                    document.body.createTextRange().select();
                    document.selection.empty();
                }
                selection.addRange(this.selectedRange);
            }
        } else if (document.selection && this.selectedRange) {
            this.selectedRange.select();
        }
    };

    // Adding Toggle HTML based on the work by @jd0000, but cleaned up a little to work in this context.
    Wysiwyg.prototype.toggleHtmlEdit = function (editor) {
        if (editor.data("wysiwyg-html-mode") !== true) {
            var oContent = editor.html();
            var editorPre = $("<pre />");
            $(editorPre).append(document.createTextNode(oContent));
            $(editorPre).attr("contenteditable", true);
            $(editor).html(" ");
            $(editor).append($(editorPre));
            $(editor).attr("contenteditable", false);
            $(editor).data("wysiwyg-html-mode", true);
            $(editorPre).focus();
        } else {
            $(editor).html($(editor).text());
            $(editor).attr("contenteditable", true);
            $(editor).data("wysiwyg-html-mode", false);
            $(editor).focus();
        }
    };


    Wysiwyg.prototype.markSelection = function (color, options) {
        this.restoreSelection();
        if (document.queryCommandSupported("hiliteColor")) {
            document.execCommand("hiliteColor", false, color || "transparent");
        }
        this.saveSelection();
    };

    //Move selection to a particular element
    function selectElementContents(element) {
        if (window.getSelection && document.createRange) {
            var selection = window.getSelection();
            var range = document.createRange();
            range.selectNodeContents(element);
            selection.removeAllRanges();
            selection.addRange(range);
        } else if (document.selection && document.body.createTextRange) {
            var textRange = document.body.createTextRange();
            textRange.moveToElementText(element);
            textRange.select();
        }
    }

    Wysiwyg.prototype.bindToolbar = function (editor, toolbar, options, toolbarBtnSelector) {
        var self = this;
        toolbar.find(toolbarBtnSelector).click(function () {
            self.restoreSelection();
            editor.focus();

            if ($(this).data(options.commandRole) === "html") {
                self.toggleHtmlEdit(editor);
            } else {
                if ($(this).data(options.commandRole) != "insertImage")
                    self.execCommand($(this).data(options.commandRole), null, editor, options, toolbarBtnSelector);
            }

            self.saveSelection();
        });

        toolbar.find("[data-toggle=dropdown]").on('click', (function () {
            self.markSelection(options.selectionColor, options);
        }));

        toolbar.on("hide.bs.dropdown", function () {
            self.markSelection(false, options);
        });

        toolbar.find("input[type=text][data-" + options.commandRole + "]").on("webkitspeechchange change", function () {
            var newValue = this.value;  // Ugly but prevents fake double-calls due to selection restoration
            this.value = "";
            self.restoreSelection();

            var text = window.getSelection();
            if (text.toString().trim() === '' && newValue) {
                //create selection if there is no selection
                self.editor.append('<span>' + newValue + '</span>');
                selectElementContents($('span:last', self.editor)[0]);
            }

            if (newValue) {
                editor.focus();
                self.execCommand($(this).data(options.commandRole), newValue, editor, options, toolbarBtnSelector);
            }
            self.saveSelection();
        }).on("blur", function () {
            var input = $(this);
            self.markSelection(false, options);
        });


        toolbar.find("[data-edit=insertImage]").on('click', function () {            
            navigationImageList();            
            $("#addImage").modal();
            $("[data-edit=closeModal]").one('click', function () { $("#addImage").modal('hide'); });

            $("[data-edit=addImage]").one('click', function () {
                if ($("#urlImg").val() != "") {                                       
                    var htmlImg = "<img src='" + $("#urlImg").val() + "' align='" + $("#alignImg").val() + "' width='" + $("#widthImg").val() + "' height='" + $("#heightImg").val() + "'>";
                    self.restoreSelection();
                    document.execCommand('insertHTML', false, htmlImg);
                    self.saveSelection();
                    $("#addImage").modal('hide');
                }
            });

        });
        
    };

    $.fn.wysiwyg = function (userOptions) {
        var wysiwyg = new Wysiwyg(this, userOptions);
    };

})(window, window.jQuery);

function navigationImageList()
{
    $("#navigationImageList span").one('click',function () {
        $.ajax({
            type: "POST",
            url: "/admin/knowBase/imageResult",
            data: '{id: ' + this.innerHTML + ' }',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                $('#imageResult').html(response);
                navigationImageList();
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
    $("#imageResult img").click(function () {
        $("#imageResult img").removeAttr("style");
        $(this).css({"border-color": "green","border-width": "2px","border-style": "solid"});
        $('#urlImg').val(this.src);
    });
}

function saveImage()
{
    if (window.FormData !== undefined) { 
        var fileUpload = $("#newImage").get(0).files;
        var fileData = new FormData();
        fileData.append("file", fileUpload[0]);
        $.ajax({  
            url: "/admin/knowBase/SaveImage",
            type: "POST",  
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            dataType: 'json',
            success: function (result) {  
                alert(result);
            },  
            error: function (err) {  
                alert(err.statusText);  
            }  
        });  
    } else {  
        alert("FormData is not supported.");  
    }  
}