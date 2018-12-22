using CLSTools;
using CLSTools.CSInterpreter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Examples{
    partial class InterpreterForm
    {
        public static void Main(){
            Start();
        }
    }
    partial class InterpreterForm{
        private System.ComponentModel.IContainer components = null;
            protected override void Dispose(bool disposing){
                if(disposing && (components != null)){
                    components.Dispose();
                }
                base.Dispose();
            }
            private Button button;
            private TextBox txtBox;
        private static void Start(){
            System.ComponentModel.ComponentResourceManager resources = 
                new System.ComponentModel.ComponentResourceManager(typeof(InterpreterForm));
            button = new Button();
            txtBox = new TextBox();

            /// button
            button.Location = new Point(0, 0);
            button.Name = "button";
            button.Size = new Size(300, 45);
            button.TabStop = false;
            button.TabIndex = 0;
            button.Text = "Interpret";
            button.UseVisualStyleBackColor = true;
            
            // InterpreterForm
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 600);
            Name = "InterpreterForm";
            Text = "C# Interpreter Form";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}