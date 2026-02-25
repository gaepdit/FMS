Imports Microsoft.VisualBasic

Public Class Class1


    Function setImagePath()
        'This is to change the map insert with the HSI number

        Dim strImagePath As String
        Dim strMDBPath As String
        Dim intSlashLoc As String

        On Error GoTo PictureNotAvailable
        strMDBPath = CurrentProject.FullName
        intSlashLoc = InStrRev(strMDBPath, "\", Len(strMDBPath))

        strImagePath = Left(strMDBPath, intSlashLoc) & "maps\h" + Trim(Str([HSI_ID])) + ".bmp"
        Me.ImageFrame.Picture = strImagePath
        GoTo sitesumrpt

PictureNotAvailable:
        strImagePath = Left(strMDBPath, intSlashLoc) & "maps\NoPicture.BMP"
        Me.ImageFrame.Picture = strImagePath


sitesumrpt:

        OSmessage = ""
        gwmessage = ""
        except2message = ""
        hsiexception = ""
        hsi10144 = ""
        gwa = ""
        gw1e = ""
        gw2em = ""
        osa = ""
        osb = ""
        os1em = ""

        'This is to handle the exceptions for soil and gw scores

        If ([HSI_ID] = 10459 Or [HSI_ID] = 10498 Or [HSI_ID] = 10689 Or [HSI_ID] = 10829) Then

            Select Case HSI_ID
                Case 10498
                    hsiexception = "The Director has determined that a release of regulated substances from groundwater has posed a threat to a water of the state."
                Case 10459, 10829, 10689
                    hsiexception = "The Director has determined that a release of regulated substances from groundwater to a water of the state has resulted in an exceedance of state water quality standards."
            End Select
            gwmessage = hsiexception
            'except2message = hsi10144
        Else

            ' Added new case to handle sources with a valid grand water score that need exception language - BGregory 12/4/2013

            If ([HSI_ID] = 10144 Or [HSI_ID] = 10747 Or [HSI_ID] = 10826 Or [HSI_ID] = 10926) Then

                Select Case HSI_ID

                    Case 10144
                        hsiexception = "Releases of Mercury and PCBs at this site have caused bioaccumulation in fish and shellfish that has resulted in the need to recommend that human consumption be limited.  A cleanup and investigation have been initiated at this site, pursuant to a CERCLA 106 removal order issued by USEPA.  The site is listed on the National Priority List. "
                    Case 10747
                        hsiexception = "The Director has determined that a release of regulated substances has occurred due to the abandonment of solid waste containing chromium, silver, zinc, and hydrochloric acid in containers, process units, and tanks at the site."
                    Case 10826
                        hsiexception = "The Director has determined that a release of regulated substances from groundwater has posed a threat to a water of the state."
                    Case 10926
                        hsiexception = "The Director has determined that a release of regulated substances from groundwater to a water of the state has resulted in an exceedance of state water quality standards."
                End Select
            End If


            'This deals with listed for Groundwater

            If gw_score > 10 Then

                Select Case gw_a
                    Case 45
                        gwa = "This site has a known release of " + gw_1d + " in groundwater at levels exceeding the reportable quantity.  "
                    Case 10
                        gwa = "This site has a suspected release of " + gw_1d + " in groundwater at levels exceeding the reportable quantity.  "
                    Case 5
                        gwa = "This site has a release of " + gw_1d + " that exceeds a reportable quantity because it has the potential to contaminate groundwater. "
                    Case Else
                        gwa = "!!!!! INVALID VALUE FOR GW_A !!!!!"
                End Select

                Select Case gw_1e
                    Case 25
                        gw1e = "This release has resulted in known human exposure greater than or equal to the MCL for " + gw_1d + ".  "
                    Case 20
                        gw1e = "This release has resulted in suspected human exposure.  "
                    Case 18
                        gw1e = "This release has resulted in known human exposure, with no MCL having been established for " + gw_1d + ".  "
                    Case 15
                        gw1e = "This release has resulted in known human exposure less than the MCL for " + gw_1d + ".  "
                    Case 12
                        gw1e = "This release has resulted in suspected human exposure.  "
                    Case 8
                        gw1e = "This release has resulted in suspected human exposure.  "
                    Case 4, 3, 2
                        gw1e = "No human exposure via drinking water is suspected from this release.  "
                    Case 1, 0
                        gw1e = ""
                    Case Else
                        gw1e = "!!!!! INVALID VALUE FOR GW_1E !!!!!"
                End Select

                If gw_1e < 8 Then

                    gw2estrt = "The nearest drinking water well is "
                    gw2eend = " from the area affected by the release.  "

                    Select Case [gw_2e]
                        Case 16
                            gw2em = "less than 0.5 miles"
                        Case 9
                            gw2em = "between 0.5 and 1 miles"
                        Case 4
                            gw2em = "between 1 and 2 miles"
                        Case 1
                            gw2em = "between 2 and 3 miles"
                        Case 0
                            gw2em = "greater than 3 miles"
                        Case Else
                            gw2em = "!!!!! INVALID VALUE FOR GW_2E !!!!!"
                    End Select
                End If

                'Added for HSI Exemption with valid GW score. BGregory 12/4/2013

                If IsNull(hsiexception) Then
                    gwmessage = gwa + gw1e + gw2estrt + gw2em + gw2eend
                Else
                    gwmessage = gwa + gw1e + gw2estrt + gw2em + gw2eend
                    except2message = hsiexception
                End If
            End If

            'This deals with listed for Soil

            If os_score <= 20 Then
                OSmessage = ""
            Else
                Select Case [os_b]
                    Case 25
                        osb = "This site has a known release of " + os_1d + " in soil at levels exceeding the reportable quantity.  "
                    Case 15
                        osb = "This site has a suspected release of " + os_1d + " in soil at levels exceeding the reportable quantity.  "
                    Case Else
                        osb = "!!!!! INVALID VALUE FOR OS_B !!!!!"

                End Select

                Select Case [os_a]
                    Case 2
                        osa = "This site has limited access.  "
                    Case 4
                        osa = "This site has unlimited access.  "
                    Case Else
                        osa = "!!!!! INVALID VALUE FOR OS_A !!!!!"

                End Select

                Select Case [os_1e]
                    Case 8
                        os1em = "less than 300 feet"
                    Case 6
                        os1em = "between 301 and 1000 feet"
                    Case 4
                        os1em = "between 1001 and 3000 feet"
                    Case 2
                        os1em = "between 3001 and 5280 feet"
                    Case Else
                        os1em = "!!!!! INVALID VALUE FOR OS-1E !!!!!"
                End Select

                OSmessage = osb + osa + "The nearest resident individual is " + os1em + " from the area affected by the release.  "
            End If
        End If
        'For Director's determination of corrective action
        If Class = "II" Then
            CAdeter = "Pending"
        Else
            CAdeter = "The Director has determined that this site requires corrective action."
        End If
        'For cleanup status

        mrrs = "Hazardous Site Response Act cleanup levels have been met for "
        mrrs5_1 = "Hazardous Site Response Act cleanup levels have been met for "
        mrrs5_2 = " through institutional and engineering controls to eliminate or reduce present and future threats to human health and the environment."
        mcip = "Cleanup activities are being conducted for "
        minv = "Investigations are being conducted to determine how much cleanup is necessary for "
        mnat = "EPD has not yet directed the responsible parties to begin investigation or cleanup under the Hazardous Site Response Act for "
        msource = "source materials"
        msoil = "soil"
        mgwater = "groundwater"
        mssgall = "source materials, soil, and groundwater"
        mssl = "source materials and soil"
        msgw = "source materials and groundwater"
        mslgw = "soil and groundwater"
        mper = "."

        If ([Source_Stat] = "RRS5") Then
            SESTAT = "5"
        Else
            SESTAT = Left([Source_Stat], 3)
        End If
        If ([Soil_stat] = "RRS5") Then
            SLSTAT = "5"
        Else
            SLSTAT = Left([Soil_stat], 3)
        End If
        If ([Ground-water_Stat] = "RRS5") Then
            GWSTAT = "5"
        Else
            GWSTAT = Left([Ground-water_Stat], 3)
        End If

        cleanupstat = SESTAT & SLSTAT & GWSTAT

        Select Case cleanupstat
            Case "555"
                '& Same Code for all 3 Status
                clup = mrrs5_1 + IIf(LF = "Y", msgw, mssgall) + mrrs5_2
            Case "RRSRRSRRS"
                clup = mrrs + IIf(LF = "Y", msgw, mssgall) + mper
            Case "CIPCIPCIP"
                clup = mcip + IIf(LF = "Y", msgw, mssgall) + mper
            Case "INVINVINV"
                clup = minv + IIf(LF = "Y", msgw, mssgall) + mper
            Case "NATNATNAT"
                clup = mnat + IIf(LF = "Y", msgw, mssgall) + mper


' Same Code for 2 of 3 Status
            Case "55RRS"
                clup = mrrs5_1 + IIf(LF = "Y", msource, mssl) + mrrs5_2 + "  " + mrrs + mgwater + mper
            Case "5RRS5"
                clup = mrrs5_1 + msgw + mrrs5_2 + "  " + IIf(LF = "Y", "", mrrs + msoil + mper)
            Case "RRS55"
                clup = mrrs5_1 + IIf(LF = "Y", mgwater, mslgw) + mrrs5_2 + "  " + mrrs + msource + mper
            Case "55CIP"
                clup = mrrs5_1 + IIf(LF = "Y", msource, mssl) + mrrs5_2 + "  " + mcip + mgwater + mper
            Case "5CIP5"
                clup = mrrs5_1 + msgw + mrrs5_2 + "  " + IIf(LF = "Y", "", mcip + msoil + mper)
            Case "CIP55"
                clup = mrrs5_1 + IIf(LF = "Y", mgwater, mslgw) + mrrs5_2 + "  " + mcip + msource + mper
            Case "55INV"
                clup = mrrs5_1 + IIf(LF = "Y", msource, mssl) + mrrs5_2 + "  " + minv + mgwater + mper
            Case "5INV5"
                clup = mrrs5_1 + msgw + mrrs5_2 + "  " + IIf(LF = "Y", "", minv + msoil + mper)
            Case "INV55"
                clup = mrrs5_1 + IIf(LF = "Y", mgwater, mslgw) + mrrs5_2 + "  " + minv + msource + mper
            Case "55NAT"
                clup = mrrs5_1 + IIf(LF = "Y", msource, mssl) + mrrs5_2 + "  " + mnat + mgwater + mper
            Case "5NAT5"
                clup = mrrs5_1 + msgw + mrrs5_2 + "  " + IIf(LF = "Y", "", mnat + msoil + mper)
            Case "NAT55"
                clup = mrrs5_1 + IIf(LF = "Y", mgwater, mslgw) + mrrs5_2 + "  " + mnat + msource + mper

            Case "RRSRRS5"
                clup = mrrs + IIf(LF = "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "RRS5RRS"
                clup = mrrs + msgw + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2)
            Case "5RRSRRS"
                clup = mrrs + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2

            Case "RRSRRSCIP"
                clup = mrrs + IIf(LF = "Y", msource, mssl) + mper + "  " + mcip + mgwater + mper
            Case "RRSCIPRRS"
                clup = mrrs + msgw + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper)
            Case "CIPRRSRRS"
                clup = mrrs + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mcip + msource + mper

            Case "RRSRRSINV"
                clup = mrrs + IIf(LF = "Y", msource, mssl) + mper + "  " + minv + mgwater + mper
            Case "RRSINVRRS"
                clup = mrrs + msgw + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper)
            Case "INVRRSRRS"
                clup = mrrs + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + minv + msource + mper

            Case "RRSRRSNAT"
                clup = mrrs + IIf(LF = "Y", msource, mssl) + mper + "  " + mnat + mgwater + mper
            Case "RRSNATRRS"
                clup = mrrs + msgw + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper)
            Case "NATRRSRRS"
                clup = mrrs + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mnat + msource + mper


            Case "CIPCIP5"
                clup = mcip + IIf(LF = "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "CIP5CIP"
                clup = mcip + msgw + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2)
            Case "5CIPCIP"
                clup = mcip + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2

            Case "CIPCIPRRS"
                clup = mcip + IIf(LF = "Y", msource, mssl) + mper + "  " + mrrs + mgwater + mper
            Case "CIPRRSCIP"
                clup = mcip + msgw + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper)
            Case "RRSCIPCIP"
                clup = mcip + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mrrs + msource + mper

            Case "CIPCIPINV"
                clup = mcip + IIf(LF = "Y", msource, mssl) + mper + "  " + minv + mgwater + mper
            Case "CIPINVCIP"
                clup = mcip + msgw + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper)
            Case "INVCIPCIP"
                clup = mcip + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + minv + msource + mper

            Case "CIPCIPNAT"
                clup = mcip + IIf(LF = "Y", msource, mssl) + mper + "  " + mnat + mgwater + mper
            Case "CIPNATCIP"
                clup = mcip + msgw + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper)
            Case "NATCIPCIP"
                clup = mcip + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mnat + msource + mper

            Case "INVINV5"
                clup = minv + IIf(LF = "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "INV5INV"
                clup = minv + msgw + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2)
            Case "5INVINV"
                clup = minv + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2

            Case "INVINVRRS"
                clup = minv + IIf(LF = "Y", msource, mssl) + mper + "  " + mrrs + mgwater + mper
            Case "INVRRSINV"
                clup = minv + msgw + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper)
            Case "RRSINVINV"
                clup = minv + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mrrs + msource + mper

            Case "INVINVCIP"
                clup = minv + IIf(LF = "Y", msource, mssl) + mper + "  " + mcip + mgwater + mper
            Case "INVCIPINV"
                clup = minv + msgw + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper)
            Case "CIPINVINV"
                clup = minv + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mcip + msource + mper

            Case "INVINVNAT"
                clup = minv + IIf(LF = "Y", msource, mssl) + mper + "  " + mnat + mgwater + mper
            Case "INVNATINV"
                clup = minv + msgw + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper)
            Case "NATINVINV"
                clup = minv + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mnat + msource + mper

            Case "NATNAT5"
                clup = mnat + IIf(LF = "Y", msource, mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "NAT5NAT"
                clup = mnat + msgw + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2)
            Case "5NATNAT"
                clup = mnat + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2

            Case "NATNATRRS"
                clup = mnat + IIf(LF = "Y", msource, mssl) + mper + "  " + mrrs + mgwater + mper
            Case "NATRRSNAT"
                clup = mnat + msgw + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper)
            Case "RRSNATNAT"
                clup = mnat + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mrrs + msource + mper

            Case "NATNATCIP"
                clup = mnat + IIf(LF = "Y", msource, mssl) + mper + "  " + mcip + mgwater + mper
            Case "NATCIPNAT"
                clup = mnat + msgw + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper)
            Case "CIPNATNAT"
                clup = mnat + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + mcip + msource + mper

            Case "NATNATINV"
                clup = mnat + IIf(LF = "Y", msource, mssl) + mper + "  " + minv + mgwater + mper
            Case "NATINVNAT"
                clup = mnat + msgw + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper)
            Case "INVNATNAT"
                clup = mnat + IIf(LF = "Y", mgwater, mslgw) + mper + "  " + minv + msource + mper

' 3 Different Status

            Case "RRS5CIP"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper
            Case "RRSCIP5"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "RRS5INV"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper
            Case "RRSINV5"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "RRS5NAT"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper
            Case "RRSNAT5"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "RRSCIPINV"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + minv + mgwater + mper
            Case "RRSINVCIP"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mcip + mgwater + mper
            Case "RRSCIPNAT"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mnat + mgwater + mper
            Case "RRSNATCIP"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mcip + mgwater + mper
            Case "RRSINVNAT"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mnat + mgwater + mper
            Case "RRSNATINV"
                clup = mrrs + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + minv + mgwater + mper


            Case "5RRSCIP"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mcip + mgwater + mper
' northside
            Case "5RRSRRS"
                clup = mrrs + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mrrs) + mslgw + mper
            Case "5CIPRRS"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "5RRSINV"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper
            Case "5INVRRS"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "5RRSNAT"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper
            Case "5NATRRS"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "5CIPINV"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + minv + mgwater + mper
            Case "5INVCIP"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mcip + mgwater + mper
            Case "5CIPNAT"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mnat + mgwater + mper
            Case "5NATCIP"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mcip + mgwater + mper
            Case "5INVNAT"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mnat + mgwater + mper
            Case "5NATINV"
                clup = mrrs5_1 + msource + mrrs5_2 + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + minv + mgwater + mper

            Case "CIPRRS5"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "CIP5RRS"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper
            Case "CIPRRSINV"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper
            Case "CIPINVRRS"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "CIPRRSNAT"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mnat + mgwater + mper
            Case "CIPNATRRS"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "CIP5INV"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper
            Case "CIPINV5"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "CIP5NAT"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper
            Case "CIPNAT5"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "CIPINVNAT"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mnat + mgwater + mper
            Case "CIPNATINV"
                clup = mcip + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + minv + mgwater + mper

            Case "INVRRS5"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "INV5RRS"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper
            Case "INVRRSCIP"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mcip + mgwater + mper
            Case "INVCIPRRS"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "INVRRSNAT"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mnat + mgwater + mper
            Case "INVNATRRS"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "INV5CIP"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper
            Case "INVCIP5"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "INV5NAT"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper
            Case "INVNAT5"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "INVCIPNAT"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mnat + mgwater + mper
            Case "INVNATCIP"
                clup = minv + msource + mper + "  " + IIf(LF = "Y", "", mnat + msoil + mper) + "  " + mcip + mgwater + mper



            Case "NATRRS5"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "NAT5RRS"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper
            Case "NATRRSCIP"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + mcip + mgwater + mper
            Case "NATCIPRRS"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "NATRRSINV"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mrrs + msoil + mper) + "  " + minv + mgwater + mper
            Case "NATINVRRS"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mrrs + mgwater + mper
            Case "NAT5CIP"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper
            Case "NATCIP5"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "NAT5INV"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper
            Case "NATINV5"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2
            Case "NATCIPINV"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", mcip + msoil + mper) + "  " + minv + mgwater + mper
            Case "NATINVCIP"
                clup = mnat + msource + mper + "  " + IIf(LF = "Y", "", minv + msoil + mper) + "  " + mcip + mgwater + mper


            Case Else
                clup = "DATA IS INCOMPLETE - PLEASE CHECK!!!!!!!!!"

        End Select

    End Function

    Private Sub Report_Current()

    End Sub

End Class
