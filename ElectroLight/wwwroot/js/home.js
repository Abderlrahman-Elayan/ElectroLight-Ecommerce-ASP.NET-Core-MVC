 <script>
        $(function () {

            // ✅ Load real cart count on page load
            $.get('/ShoppingCart/GetCartCount', function (res) {
                updateCartBadge(res.count);
            });

            // ✅ Add to cart click
            $(".add-to-cart-btn").on("click", function () {

                var button = $(this);
                var icon = button.find("i");
                var productId = button.data("product-id");

                // prevent spam clicking
                button.prop("disabled", true);

                $.ajax({
                    url: '/ShoppingCart/AddOrUpdateItem',
                    type: 'POST',
                    data: {
                        productId: productId,
                        quantity: 1
                    },

                    success: function (res) {

                        // icon animation
                        icon.removeClass("bi-cart-plus")
                            .addClass("bi-check text-success");

                        setTimeout(function () {
                            icon.removeClass("bi-check text-success")
                                .addClass("bi-cart-plus");
                        }, 1500);

                        // ✅ update REAL count
                        updateCartBadge(res.count);

                        showToast("Added to cart ✅");
                    },

                    error: function () {
                        showToast("Error adding to cart ❌");
                    },

                    complete: function () {
                        button.prop("disabled", false);
                    }
                });

            });

        });

        // ✅ badge handler (clean UX)
        function updateCartBadge(count) {
            if (count <= 0) {
                $("#cart-count").hide();
            } else {
                $("#cart-count").show().text(count);
            }
        }

        // ✅ toast
        function showToast(message) {

            var toast = $(`
                <div class="custom-toast">
                    ${message}
                </div>
            `);

            $("body").append(toast);

            setTimeout(() => toast.addClass("show"), 100);

            setTimeout(() => {
                toast.removeClass("show");
                setTimeout(() => toast.remove(), 300);
            }, 2000);
        }
    </script>