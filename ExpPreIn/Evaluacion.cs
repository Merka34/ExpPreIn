using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpPreIn
{
    public class Evaluacion
    {
        public ObservableCollection<string> Cadena { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Operador { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Errores { get; set; } = new ObservableCollection<string>();
        private string expresion;

        public string Expresion
        {
            get { return expresion; }
            set { expresion = value; }
        }

        public ICommand EvaluarCommand { get; set; }

        public Evaluacion()
        {
            EvaluarCommand = new RelayCommand(EvaluarPrefijo);
        }

        List<char> parentesis = new List<char>();
        char[] operadores = new char[]{ '+', '-', '*', '/', '^'};
        char[] bloques = new char[] { '(', ')' };
        char[] numeros = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        private void Evaluar()
        {/*
            if (i == 0)
                EvaluarPrefijo(Expresion);
            else
                EvaluarInfijo(Expresion);*/
        }
        void EvaluarPrefijo()
        {
            //A+B*C
            //bool continuar = true;
            char[] cadena = Expresion.ToCharArray(); //Convierte la expresion a un arreglo de char
            if (Cadena.Count!=0)
                Cadena.Clear();
            /*
            for (int i = 0; i < cadena.Length; i++)
            {
                if ((i%2==0 && parentesis.Count%2==0) || (i%2==1 && parentesis.Count%2==1)) // Operando
                {
                    foreach (char d in operadores)
                    {
                        if (d == cadena[i])
                        {
                            Errores.Add($"El item '{cadena[i]}' en la posicion {i} es un operador y se esperaba un operando");
                            continuar = false;
                        }
                    }
                    if (continuar) //Es un Operando
                    {

                    }
                }
            }*/
            List<string> miCad = new List<string>(); //Crear una nueva lista de string
            string mica=""; //campo que sera utilizado para recopilar los numeros
            bool num = false;//Booleano para guardar validez si el valor es numero
            for (int i = 0; i < cadena.Length; i++) //Esto es para no separar si un numero de mas de 2 cifras
            {
                num = false;
                foreach (var t in numeros) //Verifica si es un numero
                {
                    if (cadena[i]==t)
                    {
                        mica += cadena[i];
                        num = true;
                        break;
                    }
                }
                if (!num) //Si no es un numero, el valor anterior debio haber sido numero
                {
                    if (mica!="")
                    {
                        miCad.Add(mica);
                        mica = "";
                    }
                    miCad.Add(cadena[i].ToString());
                }
                else if (i==cadena.Length-1)
                {
                    if (mica != "")
                    {
                        miCad.Add(mica);
                        mica = "";
                    }
                }
            }
            
            bool operador = false;
            for (int i = 0; i < miCad.Count; i++)
            {
                operador = false;
                foreach (var g in operadores)
                {
                    if (miCad[i]==g.ToString())
                    {
                        operador = true;
                        break;
                    }
                }
                if (operador) // SI ES OPERADOR
                {
                    int idN = 0, idO = 0;
                    if (Operador.Count!=0)
                    {
                        for (int j = 0; j < operadores.Length; j++)
                        {
                            if (Operador[Operador.Count - 1]==operadores[j].ToString())
                            {
                                int y = j % 2;
                                idO = j-y;
                                break;
                            }
                        }
                        for (int k = 0; k < operadores.Length; k++)
                        {
                            if (miCad[i]==operadores[k].ToString())
                            {
                                int y = k % 2;
                                idN = k-y;
                                break;
                            }
                        }
                        if (idN > idO)
                        {
                            Operador.Add(miCad[i]);
                        }
                        else
                        {
                            Cadena.Add(Operador[Operador.Count - 1]);
                            Operador[Operador.Count - 1] = miCad[i];
                        }
                    }
                    else
                        Operador.Add(miCad[i]);
                }
                else // SI NO ES OPERANDO
                {
                    Cadena.Add(miCad[i]);
                }
            } //Fin del Ciclo

            for (int i = Operador.Count-1; i >= 0; i--)
            {
                Cadena.Add(Operador[i]);
                Operador[Operador.Count - 1] = null;
            }
        }

        void EvaluarInfijo(string a)
        {

        }
    }
}
