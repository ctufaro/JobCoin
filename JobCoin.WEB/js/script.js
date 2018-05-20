var JobCoin = function () {

    var submitted = false;    

    var WireUpUIEvents = function () {

        $("#btn-submit").click(function () {
			var entry = $("#form_addresses").val();
			if(entry.length > 0){
				$('#err-message').hide();
				var addresses = entry.split("\n")
				PopCount(addresses);
			}
			else{
				$('#err-message').show();
			} 
        });
		
		$("#btn-agree").click(function () {
            $('#deposit-address').show(); 
			submitted = true;
			Submit($("#form_addresses").val().split("\n"));
        });
		
		$("#myModal").on("hidden.bs.modal", function () {
			$('#deposit-address').hide();
			if(submitted){
				$("#form_addresses").val('');
				$("#deposit-address").html(''); 
				$("#btn-agree").prop("disabled",false);
			}
		});
    };
	
	var PopCount = function(addresses){
		var len = addresses.filter(Boolean).length;
		var msg1 = "";
		var msg2 = "";
		
		if(len==1){
			msg1 = "1 address";
			msg2= "fully distributed to the supplied address at a random time.";
		}
		else{
			msg1 = len + " addresses";
			msg2= "divided by " + len + " and distributed at random time intervals.";
		}
		$('#myModal').modal('show');
		$('#term-msg1').html(msg1);
		$('#term-msg2').html(msg2);
	}
	
    var Submit = function(addresses){
		//console.log(addresses);
		$.ajax
		({
			type: "POST", 
			//url: "http://localhost:26684/api/depositaddress",
			url: "http://ugoforapi.azurewebsites.net/api/depositaddress",
			data: { "": addresses },
			dataType: "json",
			global: false,
			error: function (xhr, error) { alert(xhr + " - " + error); },
			success: function (data) 
					{ 
						$("#form_addresses").val(''); 
						$("#deposit-address").html(data);
						$("#btn-agree").prop("disabled",true);
					}
		});
    };   

    var Init = function () {
        WireUpUIEvents();
    }();

}();


