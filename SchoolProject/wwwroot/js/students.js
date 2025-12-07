// wwwroot/js/students.js

// ⚠️ ESPERAR A QUE TODO EL DOM ESTÉ COMPLETAMENTE CARGADO
document.addEventListener('DOMContentLoaded', function () {
    initializeStudentsPage();
});

function initializeStudentsPage() {
    console.log('=== INICIALIZANDO PÁGINA ===');

    // Mostrar fecha actual
    const dateElement = document.getElementById('currentDate');
    if (dateElement) {
        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        const currentDate = new Date().toLocaleDateString('es-ES', options);
        dateElement.textContent = currentDate.charAt(0).toUpperCase() + currentDate.slice(1);
    }

    // Configurar botones de "Nuevo Estudiante"
    document.querySelectorAll('[data-bs-target="#studentModal"]').forEach(btn => {
        btn.addEventListener('click', resetCreateForm);
    });

    // Configurar botones de EDITAR
    document.querySelectorAll('.edit-btn').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            editStudent(this);
        });
    });

    // Configurar botones de ELIMINAR
    document.querySelectorAll('.delete-btn').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            showDeleteConfirmation(this);
        });
    });

    // Validación formulario de creación
    const studentForm = document.getElementById('studentForm');
    if (studentForm) {
        studentForm.addEventListener('submit', function (e) {
            if (!validateCreateForm()) {
                e.preventDefault();
            }
        });
    }

    // Validación formulario de edición
    const editStudentForm = document.getElementById('editStudentForm');
    if (editStudentForm) {
        editStudentForm.addEventListener('submit', function (e) {
            if (!validateEditForm()) {
                e.preventDefault();
            }
        });
    }

    console.log('✅ Página inicializada correctamente');
}

// ✏️ FUNCIÓN PARA EDITAR
function editStudent(button) {
    console.log('=== EDITANDO ESTUDIANTE ===');

    // 1. Obtener los datos
    const data = {
        studentId: button.getAttribute('data-id'),
        code: button.getAttribute('data-code'),
        name: button.getAttribute('data-name'),
        lastname: button.getAttribute('data-lastname'),
        birthdate: button.getAttribute('data-birthdate'),
        gender: button.getAttribute('data-gender'),
        address: button.getAttribute('data-address'),
        phone: button.getAttribute('data-phone'),
        email: button.getAttribute('data-email'),
        guardianName: button.getAttribute('data-guardianname'),
        guardianPhone: button.getAttribute('data-guardianphone'),
        enrollmentDate: button.getAttribute('data-enrollmentdate'),
        isActive: button.getAttribute('data-isactive') === 'true'
    };

    console.log('Datos:', data);

    // 2. Llenar campos hidden (que se envían al servidor)
    safeSetValue('EditStudentId', data.studentId);
    safeSetValue('EditCodeHidden', data.code);
    safeSetValue('EditNameHidden', data.name);
    safeSetValue('EditLastnameHidden', data.lastname);
    safeSetValue('EditBirthDateHidden', data.birthdate);
    safeSetValue('EditGenderHidden', data.gender);
    safeSetValue('EditEmailHidden', data.email);
    safeSetValue('EditGuardianNameHidden', data.guardianName);
    safeSetValue('EditGuardianPhoneHidden', data.guardianPhone);
    safeSetValue('EditEnrollmentDateHidden', data.enrollmentDate);

    // 3. Llenar campos visuales (solo lectura)
    safeSetValue('EditCode', data.code);
    safeSetValue('EditName', data.name);
    safeSetValue('EditLastname', data.lastname);
    safeSetValue('EditBirthDate', data.birthdate);
    safeSetValue('EditGender', data.gender);
    safeSetValue('EditEmail', data.email);
    safeSetValue('EditGuardianName', data.guardianName);
    safeSetValue('EditGuardianPhone', data.guardianPhone);
    safeSetValue('EditEnrollmentDate', data.enrollmentDate);

    // 4. Llenar campos EDITABLES
    safeSetValue('EditPhone', data.phone);
    safeSetValue('EditAddress', data.address);

    const isActiveCheckbox = document.getElementById('EditIsActive');
    if (isActiveCheckbox) {
        isActiveCheckbox.checked = data.isActive;
    }

    // 5. Limpiar validaciones previas
    document.querySelectorAll('#editStudentForm .is-invalid').forEach(el => {
        el.classList.remove('is-invalid');
    });

    // 6. Abrir el modal
    const modalElement = document.getElementById('editStudentModal');
    if (modalElement) {
        const modal = new bootstrap.Modal(modalElement);
        modal.show();
        console.log('✅ Modal abierto');
    } else {
        console.error('❌ Modal no encontrado');
    }
}

// 🔧 FUNCIÓN AUXILIAR PARA ESTABLECER VALORES DE FORMA SEGURA
function safeSetValue(elementId, value) {
    const element = document.getElementById(elementId);
    if (element) {
        element.value = value || '';
    } else {
        console.warn(`⚠️ Elemento no encontrado: ${elementId}`);
    }
}

// 🆕 FUNCIÓN PARA RESETEAR FORMULARIO DE CREACIÓN
function resetCreateForm() {
    console.log('Reseteando formulario de creación...');

    const studentForm = document.getElementById('studentForm');
    if (studentForm) {
        studentForm.reset();

        // Fecha de matrícula = hoy
        const enrollmentDate = document.getElementById('EnrollmentDate');
        if (enrollmentDate) {
            enrollmentDate.value = new Date().toISOString().split('T')[0];
        }

        // Activo por defecto
        const isActive = document.getElementById('IsActive');
        if (isActive) {
            isActive.checked = true;
        }

        // Limpiar validaciones
        document.querySelectorAll('#studentForm .is-invalid').forEach(el => {
            el.classList.remove('is-invalid');
        });
    }
}

// 🗑️ FUNCIÓN PARA CONFIRMAR ELIMINACIÓN
function showDeleteConfirmation(button) {
    const studentId = button.getAttribute('data-id');
    const studentName = button.getAttribute('data-name');

    safeSetValue('deleteStudentId', studentId);

    const nameElement = document.getElementById('studentNameToDelete');
    if (nameElement) {
        nameElement.textContent = studentName;
    }

    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
        const modal = new bootstrap.Modal(modalElement);
        modal.show();
    }
}

// ✅ VALIDAR FORMULARIO DE CREACIÓN
function validateCreateForm() {
    let isValid = true;
    const requiredFields = document.querySelectorAll('#studentForm [required]');

    requiredFields.forEach(field => {
        if (!field.value.trim()) {
            field.classList.add('is-invalid');
            isValid = false;
        } else {
            field.classList.remove('is-invalid');
        }
    });

    // Validar email
    const emailField = document.getElementById('Email');
    if (emailField && emailField.value) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(emailField.value)) {
            emailField.classList.add('is-invalid');
            isValid = false;
        }
    }

    if (!isValid) {
        alert('Por favor, completa todos los campos requeridos.');
    }

    return isValid;
}

// ✅ VALIDAR FORMULARIO DE EDICIÓN
function validateEditForm() {
    let isValid = true;

    // Solo validar teléfono y dirección
    const phone = document.getElementById('EditPhone');
    const address = document.getElementById('EditAddress');

    if (phone && !phone.value.trim()) {
        phone.classList.add('is-invalid');
        isValid = false;
    } else if (phone) {
        phone.classList.remove('is-invalid');
    }

    if (address && !address.value.trim()) {
        address.classList.add('is-invalid');
        isValid = false;
    } else if (address) {
        address.classList.remove('is-invalid');
    }

    if (!isValid) {
        alert('Por favor, completa todos los campos editables.');
    }

    return isValid;
}