$(document).ready(function () {
	/* Mega Menu */
     //   $('div.mega-menu-title').on("click", function () {
     //       console.log("call");
     //       if ($('div.mega-menu-category').is(':visible')){
     //           $('div.mega-menu-category').slideUp();
	    //	} else {
     //           $('div.mega-menu-category').slideDown();
	//	}
	//})
    $('.mega-menu-category .nav > li').hover(function(){
    	$(this).addClass("active");
		$(this).find('.popup').stop(true,true).fadeIn('slow');
    },function(){
        $(this).removeClass("active");
		$(this).find('.popup').stop(true,true).fadeOut('slow');
    })
	$('.mega-menu-category .nav > li.view-more').click(function(e){
		if($('.mega-menu-category .nav > li.more-menu').is(':visible')){
			$('.mega-menu-category .nav > li.more-menu').stop().slideUp();
			$(this).find('a').text('More category');
		} else { 
			$('.mega-menu-category .nav > li.more-menu').stop().slideDown();
			$(this).find('a').text('Close menu');
		}
		e.preventDefault();
	})
	
	/* Brands Slider */
	$("#brands .owl").owlCarousel({
		autoPlay : false,
		items : 5,
		itemsDesktop : [1199,4],
		itemsDesktopSmall : [991,3],
		itemsTablet: [767,2],
		itemsMobile : [480,2],
		slideSpeed : 1000,
		paginationSpeed : 1000,
		rewindSpeed : 1000,
		navigation : true,
		stopOnHover : true,
		pagination : false,
		scrollPerPage:true,
	});
});

function EnableMenu()
{
    if ($('div.mega-menu-category').is(':visible'))
    {
        $('div.mega-menu-category').slideUp();
	} else {
        $('div.mega-menu-category').slideDown();
	}
}

// --------- CUSTOM FUNCTIONS -----------

function fnUpdateNumberOfClicks(productId) {
    //$.ajax(
    //{
    //    url: "/Products/UpdateNumberOfClicks",
    //    type: "POST",
    //    data: { productId: productId },
    //    success: function (data) {

    //    },
    //    error: function (xhr, textStatus, error) {
    //        toastr.error(error);
    //    }
    //});
}

// --------- SHOPPING CART -----------
function fnAddCartItem(productId) {
    var quantity = 1;
    if ($("#orderQuantity").length > 0)
    {
        quantity = parseInt($("#orderQuantity").val());
    }

    $.ajax(
    {
        url: "/ShoppingCart/AddCartItem",
        type: "GET",
        data: { productId: productId, quantity: quantity },
        success: function (data) {
            //Load cart
            $.ajax(
            {
                url: "/ShoppingCart/_MyCart",
                type: "GET",
                success: function (data) {
                    $("#cart-container").html(data);
                },
                error: function (xhr, textStatus, error) {
                    toastr.error(error);
                }
            });

            toastr.success("Thêm sản phẩm vào giỏ hàng thành công!");
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
        }
    });
}

// Search web
function fnSearchWeb() {
    var postData = $("#frmSearch").serializeArray();
    fnLoadContent("search-container", $("#hdControllerName").val() + "/" + "_List", postData);
}

function fnLoadContent(container, action, parameters) {
    $('#' + container).empty().append(loadingContent);
    $.ajax({
        type: "GET",
        url: rootPath + action,
        data: parameters,
        success: function (data) {
            $("#" + container).html(data);
            $("#loadingContent").remove();
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
            $("#loadingContent").remove();
        }
    });
}

// Main search
function fnSearchMain() {
    $("div.mega-menu-category").hide();
    $(".breadcrumbs").hide();

    var postData = $("#frmSearch").serializeArray();
    fnLoadContent("search-result-container", "Home/_SearchResultList", postData);
}

function fnSearchHome() {
    $("div.mega-menu-category").hide();
    $(".breadcrumbs").hide();

    var postData = $("#frm-main-search").serializeArray();
    fnLoadContent("content-container", "Home/_SearchResult", postData);
}