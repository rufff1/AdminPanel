

// Object to store country codes and their respective formats
const countryFormats = {
    '+1': 'NPA-NXX-XXXX', // USA
    '+44': 'XXXX XXX XXXX',// UK (Example format)
    '+994': 'XX XXX XX XX'
    // Add other country formats as needed
};

const phoneInput = document.getElementById('phoneInput');
const countryCode = document.getElementById('countryCode');

phoneInput.addEventListener('input', function (event) {
    formatPhoneNumber();
});

countryCode.addEventListener('change', function () {
    formatPhoneNumber();
});

function formatPhoneNumber() {
    const selectedCountryCode = countryCode.value;
    const selectedCountryFormat = countryFormats[selectedCountryCode];
    const phoneNumber = phoneInput.value.replace(/\D/g, ''); // Remove non-digit characters

    let formattedPhoneNumber = '';

    if (phoneNumber.length > 0) {
        let currentPos = 0;
        for (let i = 0; i < selectedCountryFormat.length; i++) {
            if (selectedCountryFormat[i] === 'X') {
                formattedPhoneNumber += phoneNumber[currentPos] || '';
                currentPos++;
            } else {
                formattedPhoneNumber += selectedCountryFormat[i];
            }
        }
    }

    phoneInput.value = formattedPhoneNumber;
}