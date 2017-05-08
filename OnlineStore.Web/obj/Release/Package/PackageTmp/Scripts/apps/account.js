function Account() {
    this.LoginForm = "#frmLogin";
    this.BtnSubmitId = "#btnSubmit";
    this.BtnResetId = "#btnReset";
}

Account.prototype.bindEvents = function() {
    var $this = this;

    $(this.BtnSubmitId).click(function () {
        $this.submitForm();
    });
    
    $(this.BtnResetId).click(function () {
        $this.clearForm();
    });
};

Account.prototype.submitForm = function() {
    if (!this.isValidateForm()) {
        $(".error-message", this.LoginForm).text(Resources.Login_ErrorBlank);
    } else {
        $(this.LoginForm).submit();
    }
};

Account.prototype.clearForm = function () {
    $("input[id='UserName']", this.LoginForm).val("");
    $("input[id='Password']", this.LoginForm).val("");
};

Account.prototype.isValidateForm = function () {
    var username = $("input[id='UserName']", this.LoginForm).val();
    var password = $("input[id='Password']", this.LoginForm).val();
    if (username == "" || password == "")
        return false;
    return true;
};


