using FMS;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic

public partial class Class1
{


    public static object SetImagePath(string HSI_ID, int gw_score, int gw_a)
    {
        // This is to change the map insert with the HSI number

        string strImagePath;
        string strMDBPath;
        string intSlashLoc;
//        ;
//#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo PictureNotAva...' at character 274


                Input:

                        On Error GoTo PictureNotAvailable

                 */
        strMDBPath = CurrentProject.FullName;
        intSlashLoc = Strings.InStrRev(strMDBPath, @"\", Strings.Len(strMDBPath)).ToString();

        strImagePath = Strings.Left(strMDBPath, Conversions.ToInteger(intSlashLoc)) + (@"maps\h" + Strings.Trim(Conversion.Str(HSI_ID)) + ".bmp");
        this.ImageFrame.Picture = strImagePath;
        goto sitesumrpt;

    PictureNotAvailable:
        ;

        strImagePath = Strings.Left(strMDBPath, Conversions.ToInteger(intSlashLoc)) + @"maps\NoPicture.BMP";
        this.ImageFrame.Picture = strImagePath;


    sitesumrpt:
        ;


        string OSmessage = "";
        string gwmessage = "";
        string except2message = "";
        string hsiexception = "";
        string hsi10144 = "";
        string gwa = "";
        string gw1e = "";
        string gw2em = "";
        string osa = "";
        string osb = "";
        string os1em = "";

        // This is to handle the exceptions for soil and gw scores

        if (HSI_ID == "10459" | HSI_ID == "10498" | HSI_ID == "10689" | HSI_ID == "10829")
        {

            switch (HSI_ID)
            {
                case "10498":
                    {
                        hsiexception = "The Director has determined that a release of regulated substances from groundwater has posed a threat to a water of the state.";
                        break;
                    }
                case "10459":
                case "10829":
                case "10689":
                    {
                        hsiexception = "The Director has determined that a release of regulated substances from groundwater to a water of the state has resulted in an exceedance of state water quality standards.";
                        break;
                    }
            }
            gwmessage = hsiexception;
        }
        // except2message = hsi10144
        else
        {

            // Added new case to handle sources with a valid grand water score that need exception language - BGregory 12/4/2013

            if (HSI_ID == "10144" | HSI_ID == "10747" | HSI_ID == "10826" | HSI_ID == "10926")
            {

                switch (HSI_ID)
                {

                    case "10144":
                        {
                            hsiexception = "Releases of Mercury and PCBs at this site have caused bioaccumulation in fish and shellfish that has resulted in the need to recommend that human consumption be limited.  A cleanup and investigation have been initiated at this site, pursuant to a CERCLA 106 removal order issued by USEPA.  The site is listed on the National Priority List. ";
                            break;
                        }
                    case "10747":
                        {
                            hsiexception = "The Director has determined that a release of regulated substances has occurred due to the abandonment of solid waste containing chromium, silver, zinc, and hydrochloric acid in containers, process units, and tanks at the site.";
                            break;
                        }
                    case "10826":
                        {
                            hsiexception = "The Director has determined that a release of regulated substances from groundwater has posed a threat to a water of the state.";
                            break;
                        }
                    case "10926":
                        {
                            hsiexception = "The Director has determined that a release of regulated substances from groundwater to a water of the state has resulted in an exceedance of state water quality standards.";
                            break;
                        }
                }
            }


            // This deals with listed for Groundwater

            if (gw_score > 10)
            {

                switch (gw_a)
                {
                    case 45:
                        {
                            gwa = "This site has a known release of " + gw_1d + " in groundwater at levels exceeding the reportable quantity.  ";
                            break;
                        }
                    case 10:
                        {
                            gwa = "This site has a suspected release of " + gw_1d + " in groundwater at levels exceeding the reportable quantity.  ";
                            break;
                        }
                    case 5:
                        {
                            gwa = "This site has a release of " + gw_1d + " that exceeds a reportable quantity because it has the potential to contaminate groundwater. ";
                            break;
                        }

                    default:
                        {
                            gwa = "!!!!! INVALID VALUE FOR GW_A !!!!!";
                            break;
                        }
                }

                switch (gw_1e)
                {
                    case 25:
                        {
                            gw1e = "This release has resulted in known human exposure greater than or equal to the MCL for " + gw_1d + ".  ";
                            break;
                        }
                    case 20:
                        {
                            gw1e = "This release has resulted in suspected human exposure.  ";
                            break;
                        }
                    case 18:
                        {
                            gw1e = "This release has resulted in known human exposure, with no MCL having been established for " + gw_1d + ".  ";
                            break;
                        }
                    case 15:
                        {
                            gw1e = "This release has resulted in known human exposure less than the MCL for " + gw_1d + ".  ";
                            break;
                        }
                    case 12:
                        {
                            gw1e = "This release has resulted in suspected human exposure.  ";
                            break;
                        }
                    case 8:
                        {
                            gw1e = "This release has resulted in suspected human exposure.  ";
                            break;
                        }
                    case 4:
                    case 3:
                    case 2:
                        {
                            gw1e = "No human exposure via drinking water is suspected from this release.  ";
                            break;
                        }
                    case 1:
                    case 0:
                        {
                            gw1e = "";
                            break;
                        }

                    default:
                        {
                            gw1e = "!!!!! INVALID VALUE FOR GW_1E !!!!!";
                            break;
                        }
                }

                if (gw_1e < 8)
                {

                    string gw2estrt = "The nearest drinking water well is ";
                    string gw2eend = " from the area affected by the release.  ";

                    switch (gw_2e)
                    {
                        case 16:
                            {
                                gw2em = "less than 0.5 miles";
                                break;
                            }
                        case 9:
                            {
                                gw2em = "between 0.5 and 1 miles";
                                break;
                            }
                        case 4:
                            {
                                gw2em = "between 1 and 2 miles";
                                break;
                            }
                        case 1:
                            {
                                gw2em = "between 2 and 3 miles";
                                break;
                            }
                        case 0:
                            {
                                gw2em = "greater than 3 miles";
                                break;
                            }

                        default:
                            {
                                gw2em = "!!!!! INVALID VALUE FOR GW_2E !!!!!";
                                break;
                            }
                    }
                }

                // Added for HSI Exemption with valid GW score. BGregory 12/4/2013

                if (IsNull(hsiexception))
                {
                    gwmessage = gwa + gw1e + gw2estrt + gw2em + gw2eend;
                }
                else
                {
                    gwmessage = gwa + gw1e + gw2estrt + gw2em + gw2eend;
                    except2message = hsiexception;
                }
            }

            // This deals with listed for Soil

            if (os_score <= 20)
            {
                OSmessage = "";
            }
            else
            {
                switch (os_b)
                {
                    case 25:
                        {
                            osb = "This site has a known release of " + os_1d + " in soil at levels exceeding the reportable quantity.  ";
                            break;
                        }
                    case 15:
                        {
                            osb = "This site has a suspected release of " + os_1d + " in soil at levels exceeding the reportable quantity.  ";
                            break;
                        }

                    default:
                        {
                            osb = "!!!!! INVALID VALUE FOR OS_B !!!!!";
                            break;
                        }

                }

                switch (os_a)
                {
                    case 2:
                        {
                            osa = "This site has limited access.  ";
                            break;
                        }
                    case 4:
                        {
                            osa = "This site has unlimited access.  ";
                            break;
                        }

                    default:
                        {
                            osa = "!!!!! INVALID VALUE FOR OS_A !!!!!";
                            break;
                        }

                }

                switch (os_1e)
                {
                    case 8:
                        {
                            os1em = "less than 300 feet";
                            break;
                        }
                    case 6:
                        {
                            os1em = "between 301 and 1000 feet";
                            break;
                        }
                    case 4:
                        {
                            os1em = "between 1001 and 3000 feet";
                            break;
                        }
                    case 2:
                        {
                            os1em = "between 3001 and 5280 feet";
                            break;
                        }

                    default:
                        {
                            os1em = "!!!!! INVALID VALUE FOR OS-1E !!!!!";
                            break;
                        }
                }

                OSmessage = osb + osa + "The nearest resident individual is " + os1em + " from the area affected by the release.  ";
            }
        }
        // For Director's determination of corrective action
        if (default
#error Cannot convert IdentifierNameSyntax - see comment for details
        /* Cannot convert IdentifierNameSyntax, System.IndexOutOfRangeException: Index was outside the bounds of the array.
           at ICSharpCode.CodeConverter.CSharp.CommonConversions.CsEscapedIdentifier(String text)
           at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertIdentifier(SyntaxToken id, Boolean isAttribute, SourceTriviaMapKind sourceTriviaMapKind)
           at ICSharpCode.CodeConverter.CSharp.NameExpressionNodeVisitor.<ConvertIdentifierNameAsync>d__20.MoveNext()
        --- End of stack trace from previous location where exception was thrown ---
           at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
           at ICSharpCode.CodeConverter.CSharp.CommentConvertingVisitorWrapper.<ConvertHandledAsync>d__12`1.MoveNext()

        Input:
        Class = "II" 
         */
        )
        {
            CAdeter = "Pending";
        }
        else
        {
            CAdeter = "The Director has determined that this site requires corrective action.";
        }
        // For cleanup status

        mrrs = "Hazardous Site Response Act cleanup levels have been met for ";
        mrrs5_1 = "Hazardous Site Response Act cleanup levels have been met for ";
        mrrs5_2 = " through institutional and engineering controls to eliminate or reduce present and future threats to human health and the environment.";
        mcip = "Cleanup activities are being conducted for ";
        minv = "Investigations are being conducted to determine how much cleanup is necessary for ";
        mnat = "EPD has not yet directed the responsible parties to begin investigation or cleanup under the Hazardous Site Response Act for ";
        msource = "source materials";
        msoil = "soil";
        mgwater = "groundwater";
        mssgall = "source materials, soil, and groundwater";
        mssl = "source materials and soil";
        msgw = "source materials and groundwater";
        mslgw = "soil and groundwater";
        mper = ".";

        if (Source_Stat == "RRS5")
        {
            SESTAT = "5";
        }
        else
        {
            SESTAT = Strings.Left(Source_Stat, 3);
        }
        if (Soil_stat == "RRS5")
        {
            SLSTAT = "5";
        }
        else
        {
            SLSTAT = Strings.Left(Soil_stat, 3);
        }
        if (default
#error Cannot convert IdentifierNameSyntax - see comment for details
        /* Cannot convert IdentifierNameSyntax, System.IndexOutOfRangeException: Index was outside the bounds of the array.
           at ICSharpCode.CodeConverter.CSharp.CommonConversions.CsEscapedIdentifier(String text)
           at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertIdentifier(SyntaxToken id, Boolean isAttribute, SourceTriviaMapKind sourceTriviaMapKind)
           at ICSharpCode.CodeConverter.CSharp.NameExpressionNodeVisitor.<ConvertIdentifierNameAsync>d__20.MoveNext()
        --- End of stack trace from previous location where exception was thrown ---
           at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
           at ICSharpCode.CodeConverter.CSharp.CommentConvertingVisitorWrapper.<ConvertHandledAsync>d__12`1.MoveNext()

        Input:

         */
        )
        {
            GWSTAT = "5";
        }
        else
        {
            GWSTAT = Strings.Left(default
#error Cannot convert IdentifierNameSyntax - see comment for details
        /* Cannot convert IdentifierNameSyntax, System.IndexOutOfRangeException: Index was outside the bounds of the array.
           at ICSharpCode.CodeConverter.CSharp.CommonConversions.CsEscapedIdentifier(String text)
           at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertIdentifier(SyntaxToken id, Boolean isAttribute, SourceTriviaMapKind sourceTriviaMapKind)
           at ICSharpCode.CodeConverter.CSharp.NameExpressionNodeVisitor.<ConvertIdentifierNameAsync>d__20.MoveNext()
        --- End of stack trace from previous location where exception was thrown ---
           at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
           at ICSharpCode.CodeConverter.CSharp.CommentConvertingVisitorWrapper.<ConvertHandledAsync>d__12`1.MoveNext()

        Input:
        [Ground-water_Stat]
         */
        , 3);
        }

        cleanupstat = SESTAT + SLSTAT + GWSTAT;

        switch (cleanupstat)
        {
            case "555":
                {
                    // & Same Code for all 3 Status
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", msgw, mssgall) + mrrs5_2;
                    break;
                }
            case "RRSRRSRRS":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", msgw, mssgall) + mper;
                    break;
                }
            case "CIPCIPCIP":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", msgw, mssgall) + mper;
                    break;
                }
            case "INVINVINV":
                {
                    clup = minv + Interaction.IIf(LF == "Y", msgw, mssgall) + mper;
                    break;
                }
            case "NATNATNAT":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", msgw, mssgall) + mper;
                    break;
                }


            // Same Code for 2 of 3 Status
            case "55RRS":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", msource, mssl) + mrrs5_2 + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "5RRS5":
                {
                    clup = mrrs5_1 + msgw + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper);
                    break;
                }
            case "RRS55":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", mgwater, mslgw) + mrrs5_2 + "  " + mrrs + msource + mper;
                    break;
                }
            case "55CIP":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", msource, mssl) + mrrs5_2 + "  " + mcip + mgwater + mper;
                    break;
                }
            case "5CIP5":
                {
                    clup = mrrs5_1 + msgw + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper);
                    break;
                }
            case "CIP55":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", mgwater, mslgw) + mrrs5_2 + "  " + mcip + msource + mper;
                    break;
                }
            case "55INV":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", msource, mssl) + mrrs5_2 + "  " + minv + mgwater + mper;
                    break;
                }
            case "5INV5":
                {
                    clup = mrrs5_1 + msgw + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper);
                    break;
                }
            case "INV55":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", mgwater, mslgw) + mrrs5_2 + "  " + minv + msource + mper;
                    break;
                }
            case "55NAT":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", msource, mssl) + mrrs5_2 + "  " + mnat + mgwater + mper;
                    break;
                }
            case "5NAT5":
                {
                    clup = mrrs5_1 + msgw + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper);
                    break;
                }
            case "NAT55":
                {
                    clup = mrrs5_1 + Interaction.IIf(LF == "Y", mgwater, mslgw) + mrrs5_2 + "  " + mnat + msource + mper;
                    break;
                }

            case "RRSRRS5":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "RRS5RRS":
                {
                    clup = mrrs + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2);
                    break;
                }
            case "5RRSRRS":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                    break;
                }

            case "RRSRRSCIP":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mcip + mgwater + mper;
                    break;
                }
            case "RRSCIPRRS":
                {
                    clup = mrrs + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper);
                    break;
                }
            case "CIPRRSRRS":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mcip + msource + mper;
                    break;
                }

            case "RRSRRSINV":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + minv + mgwater + mper;
                    break;
                }
            case "RRSINVRRS":
                {
                    clup = mrrs + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper);
                    break;
                }
            case "INVRRSRRS":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + minv + msource + mper;
                    break;
                }

            case "RRSRRSNAT":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mnat + mgwater + mper;
                    break;
                }
            case "RRSNATRRS":
                {
                    clup = mrrs + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper);
                    break;
                }
            case "NATRRSRRS":
                {
                    clup = mrrs + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mnat + msource + mper;
                    break;
                }


            case "CIPCIP5":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "CIP5CIP":
                {
                    clup = mcip + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2);
                    break;
                }
            case "5CIPCIP":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                    break;
                }

            case "CIPCIPRRS":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "CIPRRSCIP":
                {
                    clup = mcip + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper);
                    break;
                }
            case "RRSCIPCIP":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mrrs + msource + mper;
                    break;
                }

            case "CIPCIPINV":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + minv + mgwater + mper;
                    break;
                }
            case "CIPINVCIP":
                {
                    clup = mcip + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper);
                    break;
                }
            case "INVCIPCIP":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + minv + msource + mper;
                    break;
                }

            case "CIPCIPNAT":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mnat + mgwater + mper;
                    break;
                }
            case "CIPNATCIP":
                {
                    clup = mcip + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper);
                    break;
                }
            case "NATCIPCIP":
                {
                    clup = mcip + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mnat + msource + mper;
                    break;
                }

            case "INVINV5":
                {
                    clup = minv + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "INV5INV":
                {
                    clup = minv + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2);
                    break;
                }
            case "5INVINV":
                {
                    clup = minv + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                    break;
                }

            case "INVINVRRS":
                {
                    clup = minv + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "INVRRSINV":
                {
                    clup = minv + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper);
                    break;
                }
            case "RRSINVINV":
                {
                    clup = minv + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mrrs + msource + mper;
                    break;
                }

            case "INVINVCIP":
                {
                    clup = minv + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mcip + mgwater + mper;
                    break;
                }
            case "INVCIPINV":
                {
                    clup = minv + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper);
                    break;
                }
            case "CIPINVINV":
                {
                    clup = minv + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mcip + msource + mper;
                    break;
                }

            case "INVINVNAT":
                {
                    clup = minv + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mnat + mgwater + mper;
                    break;
                }
            case "INVNATINV":
                {
                    clup = minv + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper);
                    break;
                }
            case "NATINVINV":
                {
                    clup = minv + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mnat + msource + mper;
                    break;
                }

            case "NATNAT5":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "NAT5NAT":
                {
                    clup = mnat + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2);
                    break;
                }
            case "5NATNAT":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                    break;
                }

            case "NATNATRRS":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "NATRRSNAT":
                {
                    clup = mnat + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper);
                    break;
                }
            case "RRSNATNAT":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mrrs + msource + mper;
                    break;
                }

            case "NATNATCIP":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + mcip + mgwater + mper;
                    break;
                }
            case "NATCIPNAT":
                {
                    clup = mnat + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper);
                    break;
                }
            case "CIPNATNAT":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + mcip + msource + mper;
                    break;
                }

            case "NATNATINV":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", msource, mssl) + mper + "  " + minv + mgwater + mper;
                    break;
                }
            case "NATINVNAT":
                {
                    clup = mnat + msgw + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper);
                    break;
                }
            case "INVNATNAT":
                {
                    clup = mnat + Interaction.IIf(LF == "Y", mgwater, mslgw) + mper + "  " + minv + msource + mper;
                    break;
                }

            // 3 Different Status

            case "RRS5CIP":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "RRSCIP5":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "RRS5INV":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper;
                    break;
                }
            case "RRSINV5":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "RRS5NAT":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "RRSNAT5":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "RRSCIPINV":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }
            case "RRSINVCIP":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "RRSCIPNAT":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "RRSNATCIP":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "RRSINVNAT":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "RRSNATINV":
                {
                    clup = mrrs + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }


            case "5RRSCIP":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }
            // northside
            case var @case when @case == "5RRSRRS":
                {
                    clup = mrrs + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mrrs) + mslgw + mper;
                    break;
                }
            case "5CIPRRS":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "5RRSINV":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }
            case "5INVRRS":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "5RRSNAT":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }
            case "5NATRRS":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "5CIPINV":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }
            case "5INVCIP":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "5CIPNAT":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "5NATCIP":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "5INVNAT":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "5NATINV":
                {
                    clup = mrrs5_1 + msource + mrrs5_2 + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }

            case "CIPRRS5":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "CIP5RRS":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "CIPRRSINV":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }
            case "CIPINVRRS":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "CIPRRSNAT":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "CIPNATRRS":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "CIP5INV":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper;
                    break;
                }
            case "CIPINV5":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "CIP5NAT":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "CIPNAT5":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "CIPINVNAT":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "CIPNATINV":
                {
                    clup = mcip + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }

            case "INVRRS5":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "INV5RRS":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "INVRRSCIP":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "INVCIPRRS":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "INVRRSNAT":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "INVNATRRS":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "INV5CIP":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "INVCIP5":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "INV5NAT":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "INVNAT5":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "INVCIPNAT":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mnat + mgwater + mper;
                    break;
                }
            case "INVNATCIP":
                {
                    clup = minv + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mnat + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }



            case "NATRRS5":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "NAT5RRS":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "NATRRSCIP":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "NATCIPRRS":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "NATRRSINV":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }
            case "NATINVRRS":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mrrs + mgwater + mper;
                    break;
                }
            case "NAT5CIP":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper;
                    break;
                }
            case "NATCIP5":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "NAT5INV":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper;
                    break;
                }
            case "NATINV5":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                    break;
                }
            case "NATCIPINV":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", mcip + msoil + mper) + "  " + minv + mgwater + mper;
                    break;
                }
            case "NATINVCIP":
                {
                    clup = mnat + msource + mper + "  " + Interaction.IIf(LF == "Y", "", minv + msoil + mper) + "  " + mcip + mgwater + mper;
                    break;
                }

            default:
                {
                    clup = "DATA IS INCOMPLETE - PLEASE CHECK!!!!!!!!!";
                    break;
                }

        }

        return default;

    }

    private void Report_Current()
    {

    }

}