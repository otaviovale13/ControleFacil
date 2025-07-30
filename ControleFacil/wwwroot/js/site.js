async function AdicionarUsuario(e) {
    e.preventDefault();

    var nome = document.getElementById("nome").value;
    var email = document.getElementById("email").value;
    var senha = document.getElementById("senha").value;
    var confirmarSenha = document.getElementById("confirmarSenha").value;

    if (!nome || !email || !senha || !confirmarSenha) {
        Swal.fire({
            icon: 'warning',
            title: 'Campos obrigatórios!',
            text: 'Por favor, preencha todos os campos.'
        });
        return;
    }

    if (senha !== confirmarSenha) {
        Swal.fire({
            icon: 'warning',
            title: 'Senha inválida!',
            text: 'As senhas não coincidem.'
        });
        return;
    }

    if (!/^\S+@\S+\.\S+$/.test(email)) {
        Swal.fire({
            icon: 'warning',
            title: 'Emial inválido!',
            text: 'Digite um email válido.'
        });
        return;
    }

    if (senha.length < 6) {
        Swal.fire({
            icon: 'warning',
            title: 'Senha inválida!',
            text: 'A senha deve ter no mínimo 6 caracteres.'
        });
        return;
    }

    const novoUsuario = {
        nome: nome,
        email: email,
        senha: senha
    };

    try {
        const response = await fetch('/Login/AdicionarUsuario', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(novoUsuario)
        });

        if (response.ok) {
            const data = await response.json();
            Swal.fire({
                icon: 'success',
                title: 'Cadastro realizado!',
                text: `Bem-vindo, ${data.nome}.`
            }).then(() => {
                window.location.href = `/Home/Index/${data.id}`;
            });
        } else if (response.status === 409) {
            Swal.fire({
                icon: 'warning',
                title: 'Erro ao cadastrar!',
                text: 'Email já está cadastrado.'
            });
            return;
        } else {
            Swal.fire({
                icon: 'warning',
                title: 'Erro!',
                text: 'Erro ao criar usuário.'
            });
            return;
        }
    } catch (error) {
        Swal.fire({
            icon: 'warning',
            title: 'Erro na requisição!',
            text: error.message
        });
        return;
    }
}

async function LogarUsuario(e) {
    e.preventDefault();

    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;

    if (!email || !senha) {
        Swal.fire({
            icon: 'warning',
            title: 'Campos obrigatórios!',
            text: 'Por favor, preencha todos os campos.'
        });
        return;
    }

    if (!/^\S+@\S+\.\S+$/.test(email)) {
        Swal.fire({
            icon: 'warning',
            title: 'Email inválido!',
            text: 'Digite um email válido.'
        });
        return;
    }

    if (senha.length < 6) {
        Swal.fire({
            icon: 'warning',
            title: 'Senha inválida!',
            text: 'A senha deve ter no mínimo 6 caracteres.'
        });
        return;
    }

    const usuario = { email, senha };

    try {
        const response = await fetch('/Login/LogarUsuario', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(usuario)
        });

        if (response.ok) {
            const data = await response.json();
            Swal.fire({
                icon: 'success',
                title: 'Login realizado!',
                text: `Bem-vindo, ${data.nome}.`
            }).then(() => {
                window.location.href = `/Home/Index/${data.id}`;
            });
        } else if (response.status === 409) {
            const error = await response.text();
            Swal.fire({
                icon: 'error',
                title: 'Erro de login!',
                text: error
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Erro!',
                text: 'Erro inesperado no login.'
            });
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Erro na requisição!',
            text: error.message
        });
    }
}

async function EnviarLinkRecuperacao(e) {
    e.preventDefault();

    const email = document.getElementById("email").value;

    if (!email) {
        Swal.fire({
            icon: 'warning',
            title: 'Campos obrigatórios!',
            text: 'Por favor, preencha todos os campos.'
        });
        return;
    }

    if (!/^\S+@\S+\.\S+$/.test(email)) {
        Swal.fire({
            icon: 'warning',
            title: 'Email inválido!',
            text: 'Digite um email válido.'
        });
        return;
    }

    const usuario = { email };

    try {
        const response = await fetch('/Login/EnviarLinkRecuperacao', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(usuario)
        });

        if (response.ok) {
            Swal.fire({
                icon: 'success',
                title: 'Email enviado!',
                text: `Um link foi enviado para o seu e-mail.`
            });
            document.getElementById("email").value = "";
        } else if (response.status === 409) {
            const error = await response.text();
            Swal.fire({
                icon: 'error',
                title: 'Erro ao enviar!',
                text: error
            });
            document.getElementById("email").value = "";
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Erro!',
                text: 'Erro inesperado ao enviar.'
            });
            document.getElementById("email").value = "";
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Erro na requisição!',
            text: error.message
        });
        document.getElementById("email").value = "";
    }
}

async function SalvarNovaSenha(e) {
    e.preventDefault();

    var senha = document.getElementById("senha").value;
    var confirmarSenha = document.getElementById("confirmarSenha").value;

    if (!senha || !confirmarSenha) {
        Swal.fire({
            icon: 'warning',
            title: 'Campos obrigatórios!',
            text: 'Por favor, preencha todos os campos.'
        });
        return;
    }

    if (senha !== confirmarSenha) {
        Swal.fire({
            icon: 'warning',
            title: 'Senha inválida!',
            text: 'As senhas não coincidem.'
        });
        return;
    }

    if (senha.length < 6) {
        Swal.fire({
            icon: 'warning',
            title: 'Senha inválida!',
            text: 'A senha deve ter no mínimo 6 caracteres.'
        });
        return;
    }

    const novaSenha = {
        senha: senha
    };

    try {
        const token = document.querySelector("input[name='token']").value;

        const response = await fetch(`/Login/SalvarNovaSenha?token=${encodeURIComponent(token)}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(novaSenha)
        });

        if (response.ok) {
            Swal.fire({
                icon: 'success',
                title: 'Senha alterada!',
                text: `Senha alterada com sucesso.`
            }).then(() => {
                window.location.href = `/Login/Index`;
            });
        } else if (response.status === 409) {
            Swal.fire({
                icon: 'warning',
                title: 'Erro no token!',
                text: error
            });

            document.getElementById("senha").value = "";
            document.getElementById("confirmarSenha").value = "";
            return;
        } else {
            Swal.fire({
                icon: 'warning',
                title: 'Erro!',
                text: 'Erro ao alterar senha.'
            });

            document.getElementById("senha").value = "";
            document.getElementById("confirmarSenha").value = "";
            return;
        }
    } catch (error) {
        Swal.fire({
            icon: 'warning',
            title: 'Erro na requisição',
            text: error.message
        });

        document.getElementById("senha").value = "";
        document.getElementById("confirmarSenha").value = "";
        return;
    }
}