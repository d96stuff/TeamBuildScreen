// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelAtom.cs" company="Linn Products Limited">
//   Copyright © 2011 Linn Products Limited. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models.Labels
{
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [System.SerializableAttribute]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(TypeName = "hudson.model.labels.LabelAtom")]
    public class LabelAtom : Label
    {
        #region Constants and Fields

        private LabelAtomProperty[] propertiesListField;

        #endregion

        #region Properties

        [XmlElement("propertiesList", Form = XmlSchemaForm.Unqualified)]
        public LabelAtomProperty[] PropertiesList
        {
            get
            {
                return this.propertiesListField;
            }

            set
            {
                this.propertiesListField = value;
            }
        }

        #endregion
    }
}