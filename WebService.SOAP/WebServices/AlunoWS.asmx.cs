using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Serialization;
using System.Data;
//using WebService.SOAP.Model;

namespace WebService.SOAP.WebServices
{
    /// <summary>
    /// Descrição resumida de AlunoWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class AlunoWS : System.Web.Services.WebService
    {
        Aluno alunos = new Aluno();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public xml ObterAlunosPorNome(string nome)
        {
            var result = alunos.ObterAlunoPeloNome(nome);

            //Popular a Classe xml
            xml dadosXML = new xml(result.ToList());
            //Retornar o xml
            return dadosXML;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public xml ObterAlunos()
        {

            //Popular a Classe xml
            xml dadosXML = new xml(alunos.ObterAlunos());
            //Retornar o xml
            return dadosXML;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public void IncluirAluno(string nome, string curso, string semestre, int ra, string cpf, string cidade)
        {
            Aluno aluno = new Aluno()
            {
                NomeAluno = nome,
                Curso = curso,
                Semestre = semestre,
                RA = ra,
                CPF = cpf,
                Cidade = cidade
            };
            alunos.IncluirAluno(aluno);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public void RemoverAluno(string nome)
        {
            alunos.RemoverAluno(nome);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public void EditarAluno(string nomeAtual, string nome, string curso, string semestre, int ra, string cpf, string cidade)
        {
            alunos.EditarAluno(nomeAtual, nome, curso, semestre, ra, cpf, cidade);
        }
    }

    public class Aluno
    {
        [XmlElement]
        public string NomeAluno { get; set; }
        [XmlElement]
        public string Curso { get; set; }
        [XmlElement]
        public string Semestre { get; set; }
        [XmlElement]
        public int RA { get; set; }
        [XmlElement]
        public string CPF { get; set; }
        [XmlElement]
        public string Cidade { get; set; }

        static List<Aluno> alunos = new List<Aluno>();

        public List<Aluno> ObterAlunos()
        {
            return alunos;
        }

        public IEnumerable<Aluno> ObterAlunoPeloNome(string nome)
        {
        IEnumerable<Aluno> list = alunos.Where(x => x.NomeAluno == nome);
         return list;
        }

        public void IncluirAluno(Aluno aluno)
        {
        alunos.Add(aluno);
        }
        public void RemoverAluno(string nome)
        {
            Aluno aluno = alunos.Find(x => x.NomeAluno == nome);
            alunos.Remove(aluno);
        }

        public void EditarAluno(string nomeAtual, string nome, string curso, string semestre, int ra, string cpf, string cidade)
        {
            Aluno aluno = alunos.FirstOrDefault(a => a.NomeAluno == nomeAtual);
            if (aluno != null)
            {
                    aluno.NomeAluno = nome;
                    aluno.Curso = curso;
                    aluno.Semestre = semestre;
                    aluno.RA = ra;
                    aluno.CPF = cpf;
                    aluno.Cidade = cidade;
            }
        }
    }

    [XmlRoot()]
    public class xml
    {
        public xml() { }
        public xml(List<Aluno> alunos)
        {
            this.alunos = alunos;
        }
        public List<Aluno> alunos { get; set; }
    }
}
