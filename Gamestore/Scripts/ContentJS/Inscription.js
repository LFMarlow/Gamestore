function inscriptionClick(event) {
    sanitizeInscription(event);
}

function sanitizeInscription(event) {
    var form = document.forms[0];
    var nom = form['MainContent_TxtBoxNom'];
    var prenom = form['MainContent_TxtBoxPrenom'];
    var email = form['MainContent_TxtBoxMail'];
    var password = form['MainContent_TxtBoxMDP'];
    var adresse = form['MainContent_TxtBoxAP'];

    
    var originalNom = nom.value;
    var originalPrenom = prenom.value;
    var originalEmail = email.value;
    var originalPassword = password.value;
    var originalAdresse = adresse.value;

    nom.value = DOMPurify.sanitize(nom.value);
    prenom.value = DOMPurify.sanitize(prenom.value);
    email.value = DOMPurify.sanitize(email.value);
    password.value = DOMPurify.sanitize(password.value);
    adresse.value = DOMPurify.sanitize(adresse.value);

    
    if (originalNom !== nom.value || originalPrenom !== prenom.value ||
        originalEmail !== email.value || originalPassword !== password.value ||
        originalAdresse !== adresse.value) {
        alert("Des éléments suspects ont été détectés et nettoyés. Veuillez vérifier vos informations avant de soumettre à nouveau.");
        event.preventDefault();
    }
}
