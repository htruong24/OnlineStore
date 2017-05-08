function NewsManagementEdit() {
    this.btnUpdate = "#btnUpdate";

    this.formContent = {        
        formId: "#frmEdit",
        txtHeadline: "#txtHeadline",
        txtSubHeadline: "#txtSubHeadline",
        txtBody: "#txtBody",
        divTopicContent: ".topic-list"
    };

    this.brefNewsModel = {        
        NewsItemId: -1,
        Headline: "",
        SubHeadline: "",
        Body: "",
        NewsTopics: null
    };
}

NewsManagementEdit.prototype.getBrefNewsModel = function () {
    var $this = this;
    
    this.brefNewsModel.Headline = $(this.formContent.txtHeadline).val();
    this.brefNewsModel.SubHeadline = $(this.formContent.txtSubHeadline).val();
    this.brefNewsModel.Body = $(this.formContent.txtBody).val();
    
    var chosens = $("select.chosen-select option:selected", this.formContent.divTopicContent);

    var arr = [];
    chosens.each(function (e) {
        var newsTopic = {
            NewsItemId: $this.brefNewsModel.NewsItemId,
            TopicId: common.parseInt($(this).val())
        };
        arr.push(newsTopic);
    });

    this.brefNewsModel.NewsTopics = arr;
};

NewsManagementEdit.prototype.bindEvents = function () {
    var $this = this;
    $(this.btnUpdate).click(function(e) {
        $('#dialog').html(Resources.NewsManagement_UpdateNewsMessage);
        $("#dialog").dialog({
            resizable: false,
            title: Resources.UserManagement_Update,
            height: 'auto',
            modal: true,
            buttons: {
                "更新": function () {
                    $this.getBrefNewsModel();
                    $.ajax({
                        type: "POST",
                        contentType: 'application/json',
                        url: '/NewsManagement/UpdateBriefNew',
                        cache: false,
                        data: JSON.stringify($this.brefNewsModel),
                        success: function (data) {
                            if (data.ErrorCode == "0") {
                               // jAlert("Update successfully!", 'Success', function () {
                                    window.location.href = $($this.btnUpdate).attr("data-url");
                               // });
                            } else {
                                jAlert(data.ErrorMessage, Resources.Notice);
                            }
                        },
                        error: function (jqXHR, error, errorThrown) {
                            jAlert(error, Resources.Notice);
                        }
                    });

                    $(this).dialog("close");
                },
                "戻る": function () {
                    $(this).dialog("close");
                }
            }
        });
    });
};

NewsManagementEdit.prototype.getTopics = function () {
    var $this = this;
    $.ajax({
        type: "POST",
        contentType: 'application/json',
        url: '/NewsManagement/GetTopicMasters',
        async: false,
        cache: false,
        success: function (data) {
            if (data != null) {
                if (data.ErrorCode == "0") {
                    if (data.Result != null && data.Result.length > 0) {
                        var topicContent = $($this.formContent.divTopicContent);
                        topicContent.empty();
                        var html = $this.getTopicHtml(data);
                        topicContent.append(html);
                        
                        $this.getChosenUi();
                    }
                } else {
                    jAlert(data.ErrorMessage, Resources.Notice);
                }
            }
        },
        error: function (jqXHR, error, errorThrown) { }
    });
};

NewsManagementEdit.prototype.getTopicHtml = function (data) {
    var arr = [];
    for (var i = 0; i < data.Result.length; i++) {
        var item = data.Result[i];
        var classificationName = item.ClassificationName == null ? "Others" : item.ClassificationName;

        arr.push('<div class="col-md-6">');
        arr.push('<h4><b>' + classificationName + '</b></h4>');
        arr.push('<select id="' + item.ClassificationType + '" data-placeholder="選択する" multiple class="chosen-select">');
        
        for (var j = 0; j < item.TopicMasters.length; j++) {
            var topic = item.TopicMasters[j];
            arr.push('<option value="' + topic.TopicId + '" >' + topic.TopicName + '</option>');

        }
        arr.push('</select>');
        arr.push('</div>');
    }
    return arr.join('');
};

NewsManagementEdit.prototype.getChosenUi = function () {
    var content = $(this.formContent.divTopicContent);
    var chosens = $("select.chosen-select", content);
    chosens.chosen({ width: "100%", allow_single_deselect: true });

    if (this.brefNewsModel.NewsTopics) {
        for (var i = 0; i < this.brefNewsModel.NewsTopics.length; i++) {
            var topic = this.brefNewsModel.NewsTopics[i];

            $("option[value='" + topic.TopicId + "']", chosens).prop('selected', true);
        }
    }
    chosens.trigger('chosen:updated');
};